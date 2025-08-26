using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyFirstMvc.Filters
{
    /// <summary>
    /// Action Filter 示例 - 记录 Action 执行时间和参数验证
    /// </summary>
    public class ActionTimingFilter : IActionFilter
    {
        private Stopwatch? _stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;
            
            Console.WriteLine($"[ActionFilter] OnActionExecuting - Starting {controllerName}.{actionName}");
            
            // 记录传入的参数
            if (context.ActionArguments.Any())
            {
                Console.WriteLine($"[ActionFilter] Action Arguments:");
                foreach (var arg in context.ActionArguments)
                {
                    Console.WriteLine($"  - {arg.Key}: {arg.Value}");
                }
            }

            // 可以在这里修改传入的参数
            if (context.ActionArguments.ContainsKey("message"))
            {
                var originalMessage = context.ActionArguments["message"]?.ToString();
                context.ActionArguments["message"] = $"[Modified by ActionFilter] {originalMessage}";
                Console.WriteLine($"[ActionFilter] Modified 'message' parameter");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch?.Stop();
            var elapsedTime = _stopwatch?.ElapsedMilliseconds ?? 0;
            
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;
            
            Console.WriteLine($"[ActionFilter] OnActionExecuted - {controllerName}.{actionName} completed in {elapsedTime}ms");
            
            // 检查是否有异常
            if (context.Exception != null)
            {
                Console.WriteLine($"[ActionFilter] Exception occurred: {context.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"[ActionFilter] Action executed successfully");
            }

            // 可以在这里修改 Action 的结果
            // 例如：添加额外的响应头
            context.HttpContext.Response.Headers.Append("X-Action-Execution-Time", elapsedTime.ToString());
        }
    }
}