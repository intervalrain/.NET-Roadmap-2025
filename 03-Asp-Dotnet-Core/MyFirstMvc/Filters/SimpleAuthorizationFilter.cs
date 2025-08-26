using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFirstMvc.Filters
{
    /// <summary>
    /// Authorization Filter 示例 - 检查请求是否包含特定的 Header
    /// </summary>
    public class SimpleAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 检查请求是否包含 "X-API-Key" header
            if (!context.HttpContext.Request.Headers.ContainsKey("X-API-Key"))
            {
                // 如果没有 API Key，则返回未授权
                context.Result = new UnauthorizedObjectResult(new { 
                    message = "Missing X-API-Key header. Authorization Filter blocked this request." 
                });
                return;
            }

            var apiKey = context.HttpContext.Request.Headers["X-API-Key"].ToString();
            
            // 简单验证 API Key (实际项目中应该从数据库或配置中验证)
            if (apiKey != "demo-api-key-12345")
            {
                context.Result = new UnauthorizedObjectResult(new { 
                    message = "Invalid API Key. Authorization Filter blocked this request." 
                });
                return;
            }

            // 如果验证通过，继续执行后续的 Filter 和 Action
            Console.WriteLine($"[AuthorizationFilter] Request authorized with API Key: {apiKey}");
        }
    }
}