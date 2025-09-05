using HR.LeaveManagement.Api.Middleware.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Net.Http.Json;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // we define and specific status code and a default problem 
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            // We have a standard where we have a problem details object that is globally accepted way of representing problems in APIs or error responses in APIs.
            CustomProblemDetails problem = new();

            switch (ex)
            {// here we put all the custom exception we create for which we want to throw an scpecif set of actions
                case Application.Exceptions.BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    // We have a standard where we have a problem details object that is globally accepted way of representing problems in APIs or error responses in APIs. 
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(Application.Exceptions.BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case Application.Exceptions.NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    // We have a standard where we have a problem details object that is globally accepted way of representing problems in APIs or error responses in APIs. 
                    problem = new CustomProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(Application.Exceptions.NotFoundException),
                        Detail = NotFound.InnerException?.Message,
                        
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(Application.Exceptions.NotFoundException),
                        Detail = ex.StackTrace,
                    };
                    break;

            }
            // just in case Status code is changed to set in the response what is the new one.
            // We are setting the status code we want to return
            httpContext.Response.StatusCode = (int)statusCode;
            // And we are writting the problem details as JSON back with the response. 
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
