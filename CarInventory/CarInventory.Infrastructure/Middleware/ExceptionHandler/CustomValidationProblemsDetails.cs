using Microsoft.AspNetCore.Mvc;

namespace CarInventory.Infrastructure.Middleware.ExceptionHandler
{
    public class CustomValidationProblemsDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
