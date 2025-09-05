using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middleware.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        // it is a readonly property that gets set when a bad request is returned from the API 
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
