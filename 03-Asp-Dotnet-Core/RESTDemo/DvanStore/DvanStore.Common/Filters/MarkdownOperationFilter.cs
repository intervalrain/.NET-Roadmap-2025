using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Annotations;

namespace DvanStore.Common.Filters;

/// <summary>
/// Swagger operation filter to load markdown descriptions
/// </summary>
public class MarkdownOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var swaggerOperationAttribute = context.MethodInfo
            .GetCustomAttributes(typeof(SwaggerOperationAttribute), false)
            .FirstOrDefault() as SwaggerOperationAttribute;

        if (swaggerOperationAttribute?.Description != null)
        {
            var markdownContent = MarkdownService.LoadMarkdown(swaggerOperationAttribute.Description);
            if (!markdownContent.StartsWith("Documentation file") && !markdownContent.StartsWith("Error loading"))
            {
                operation.Description = markdownContent;
            }
        }
    }
}