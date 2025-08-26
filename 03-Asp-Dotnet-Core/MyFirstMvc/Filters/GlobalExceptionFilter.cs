using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MyFirstMvc.Filters
{
    /// <summary>
    /// Exception Filter 示例 - 全域異常處理
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"[ExceptionFilter] Caught exception: {context.Exception.GetType().Name}");
            Console.WriteLine($"[ExceptionFilter] Exception message: {context.Exception.Message}");
            Console.WriteLine($"[ExceptionFilter] Stack trace: {context.Exception.StackTrace}");

            // 根據不同的異常類型，回傳不同的錯誤訊息
            var response = context.Exception switch
            {
                ArgumentException argEx => new ErrorResponse
                {
                    Message = "Invalid argument provided",
                    Detail = argEx.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                },
                InvalidOperationException invEx => new ErrorResponse
                {
                    Message = "Invalid operation",
                    Detail = invEx.Message,
                    StatusCode = (int)HttpStatusCode.Conflict
                },
                UnauthorizedAccessException => new ErrorResponse
                {
                    Message = "Access denied",
                    Detail = "You don't have permission to access this resource",
                    StatusCode = (int)HttpStatusCode.Forbidden
                },
                _ => new ErrorResponse
                {
                    Message = "An unexpected error occurred",
                    Detail = "Please try again later or contact support",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                }
            };

            // 設定回應
            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

            // 標記異常已處理，防止它繼續向上拋出
            context.ExceptionHandled = true;
            
            Console.WriteLine($"[ExceptionFilter] Exception handled, returning {response.StatusCode} status");
        }

        private class ErrorResponse
        {
            public string Message { get; set; } = string.Empty;
            public string Detail { get; set; } = string.Empty;
            public int StatusCode { get; set; }
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        }
    }
}