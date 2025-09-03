using Microsoft.AspNetCore.Mvc;

namespace DvanStore.Common;

public static class ProblemDetailFactory
{
    public static IActionResult ProblemDetailModelState(ActionContext context)
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var response = new
        {
            message = "Validation failed",
            errors
        };

        return new BadRequestObjectResult(response);   
    }
}