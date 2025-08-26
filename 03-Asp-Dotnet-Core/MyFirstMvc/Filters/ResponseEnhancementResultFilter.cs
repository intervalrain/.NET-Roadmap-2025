using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFirstMvc.Filters
{
    /// <summary>
    /// Result Filter 示例 - 增強 HTTP 回應
    /// </summary>
    public class ResponseEnhancementResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"[ResultFilter] OnResultExecuting - About to execute result");
            
            // 在結果執行前，可以修改回應的 Headers
            context.HttpContext.Response.Headers.Append("X-Custom-Server", "MyFirstMvc-Server");
            context.HttpContext.Response.Headers.Append("X-Response-Generated", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"));
            
            // 如果是 JSON 回應，可以修改回應內容
            if (context.Result is ObjectResult objectResult)
            {
                Console.WriteLine($"[ResultFilter] Modifying ObjectResult response");
                
                // 包裝原始回應，添加元數據
                var enhancedResponse = new
                {
                    success = true,
                    timestamp = DateTime.UtcNow,
                    data = objectResult.Value,
                    meta = new
                    {
                        server = "MyFirstMvc",
                        version = "1.0.0",
                        processedBy = "ResultFilter"
                    }
                };
                
                objectResult.Value = enhancedResponse;
            }
            else if (context.Result is ViewResult viewResult)
            {
                Console.WriteLine($"[ResultFilter] Processing ViewResult for view: {viewResult.ViewName}");
                
                // 為 View 添加額外的 ViewData
                if (viewResult.ViewData != null)
                {
                    viewResult.ViewData["FilterProcessedTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    viewResult.ViewData["ProcessedByFilter"] = "ResponseEnhancementResultFilter";
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"[ResultFilter] OnResultExecuted - Result execution completed");
            
            // 記錄回應的狀態碼
            var statusCode = context.HttpContext.Response.StatusCode;
            Console.WriteLine($"[ResultFilter] Response status code: {statusCode}");
            
            // 檢查是否在執行結果時發生異常
            if (context.Exception != null)
            {
                Console.WriteLine($"[ResultFilter] Exception during result execution: {context.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"[ResultFilter] Result executed successfully");
            }
            
            // 注意：在 OnResultExecuted 中，Response 可能已经开始发送，無法再添加 Headers
            // 所以我们只记录日志，不添加 Header
            Console.WriteLine($"[ResultFilter] Response completed, cannot add headers at this stage");
        }
    }
}