using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly SignInManager<ApplicationUser> _singInManager;

        public AuthService(UserManager<ApplicationUser> _UserManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> singInManager)
        {
            _userManager = _UserManager;
            _jwtSettings = jwtSettings;
            _singInManager = singInManager;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            // We can find the user by whichever method we choose
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException($"User wit {request.Email} not found.", request.Email);
            }
            // SignInManager is what we use in web applications to sign you in and it creates the cookie and everything.
            // this result is going to be send it to the API. CheckPasswordSignInAsync is saying Could this user that i found earlier (Line 32)
            // actually sign in with this password? and if yes we get true at the result and if not we get false.
            var result = await _singInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{request.Email} aren't valid'. ");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            // This is the user object
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // If the creation was succesful we would add it to a role.
                // here we are only allowing users to be registered as Employee not administrators
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else 
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("{0}\n", err.Description);
                }
                throw new BadRequestException($"{str}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //Users claims are key-value pairs that tell information 'bout the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            // Once we send over the token, everything will be seen as a claim.
            // Claims are used to determine your permissions in the client app.

            // It says Give me all the strings and select them into new objects of type Claim.
            // and then we have the static claim type Role 
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {   // Sub indicates the user like user id or userName
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                // This is to give to the token an unique id.
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            // This is the Symetric security key.
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Value.Issuer,
                    audience: _jwtSettings.Value.Issuer,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtSettings.Value.DurationInMinutes),
                    signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }
    }
}
