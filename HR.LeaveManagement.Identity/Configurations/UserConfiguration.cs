using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //var hasher = new PasswordHasher<ApplicationUser>();   This cannot be used because it makes the hash dynamic
            builder.HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = "AQAAAAIAAYagAAAAEHTpnpv+i+I9mS6v4xd7q29DJEIU+H6DotE2xDvuqwll7wgpxdyjaI69QsYVga3qcw==", //the hashed password is P@ssword1
                    EmailConfirmed = true,
                    ConcurrencyStamp = "11111111-1111-1111-1111-111111111111", // <--- fijo
                    SecurityStamp = "22222222-2222-2222-2222-222222222222"  // <--- fijo
                },
                 new ApplicationUser
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                     Email = "user@localhost.com",
                     NormalizedEmail = "USER@LOCALHOST.COM",
                     FirstName = "System",
                     LastName = "User",
                     UserName = "user@localhost.com",
                     NormalizedUserName = "USER@LOCALHOST.COM",
                     PasswordHash = "AQAAAAIAAYagAAAAEHTpnpv+i+I9mS6v4xd7q29DJEIU+H6DotE2xDvuqwll7wgpxdyjaI69QsYVga3qcw==", //the hashed password is P@ssword1
                     EmailConfirmed = true,
                     ConcurrencyStamp = "33333333-3333-3333-3333-333333333333", // <--- fijo
                     SecurityStamp = "44444444-4444-4444-4444-444444444444"
                 }
                );
        }
    }
}
