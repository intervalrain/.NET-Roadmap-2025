using Microsoft.AspNetCore.Mvc;
using MyFirstMvc.Filters;

namespace MyFirstMvc.Controllers;

/// <summary>
/// 演示所有 5 種 Filter 功能的 Controller
/// </summary>
[ServiceFilter(typeof(GlobalExceptionFilter))]      // Exception Filter
[ServiceFilter(typeof(ResponseEnhancementResultFilter))]  // Result Filter  
public class FilterDemoController : Controller
{
    private readonly ILogger<FilterDemoController> _logger;

    public FilterDemoController(ILogger<FilterDemoController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 演示 Action Filter - 正常執行
    /// </summary>
    [ServiceFilter(typeof(ActionTimingFilter))]  // Action Filter
    public IActionResult ActionFilterDemo(string? message = "Hello from Action Filter!")
    {
        _logger.LogInformation("ActionFilterDemo executed with message: {Message}", message);
        
        return Json(new { 
            message = message,
            controller = "FilterDemo",
            action = "ActionFilterDemo",
            timestamp = DateTime.Now
        });
    }

    /// <summary>
    /// 演示 Resource Filter (緩存功能)
    /// </summary>
    [ServiceFilter(typeof(CacheResourceFilter))]  // Resource Filter
    [ServiceFilter(typeof(ActionTimingFilter))]   // Action Filter
    public IActionResult ResourceFilterDemo(string data = "cached-data")
    {
        _logger.LogInformation("ResourceFilterDemo executed - this should be cached!");
        
        // 模擬一個耗時的操作
        Thread.Sleep(1000);
        
        return Json(new { 
            data = data,
            message = "This response should be cached for 30 seconds",
            generatedAt = DateTime.Now,
            controller = "FilterDemo",
            action = "ResourceFilterDemo"
        });
    }

    /// <summary>
    /// 演示 Authorization Filter - 需要 X-API-Key header
    /// </summary>
    [ServiceFilter(typeof(SimpleAuthorizationFilter))]  // Authorization Filter
    [ServiceFilter(typeof(ActionTimingFilter))]         // Action Filter
    public IActionResult AuthorizationFilterDemo()
    {
        _logger.LogInformation("AuthorizationFilterDemo executed - user was authorized!");
        
        return Json(new { 
            message = "You are authorized! This action requires X-API-Key header.",
            controller = "FilterDemo",
            action = "AuthorizationFilterDemo",
            timestamp = DateTime.Now
        });
    }

    /// <summary>
    /// 演示 Exception Filter - 故意拋出異常
    /// </summary>
    [ServiceFilter(typeof(ActionTimingFilter))]  // Action Filter
    public IActionResult ExceptionFilterDemo(string exceptionType = "general")
    {
        _logger.LogInformation("ExceptionFilterDemo - about to throw {ExceptionType} exception", exceptionType);
        
        // 根據參數拋出不同類型的異常來測試 Exception Filter
        switch (exceptionType.ToLower())
        {
            case "argument":
                throw new ArgumentException("This is a test argument exception from ExceptionFilterDemo");
            
            case "invalidoperation":
                throw new InvalidOperationException("This is a test invalid operation exception from ExceptionFilterDemo");
            
            case "unauthorized":
                throw new UnauthorizedAccessException("This is a test unauthorized access exception from ExceptionFilterDemo");
            
            default:
                throw new Exception("This is a test general exception from ExceptionFilterDemo");
        }
    }

    /// <summary>
    /// 演示 Result Filter - 返回 View 而不是 JSON
    /// </summary>
    [ServiceFilter(typeof(ActionTimingFilter))]              // Action Filter
    [ServiceFilter(typeof(ResponseEnhancementResultFilter))] // Result Filter
    public IActionResult ResultFilterViewDemo()
    {
        _logger.LogInformation("ResultFilterViewDemo executed - returning a View");
        
        ViewBag.Message = "This view was processed by Result Filter";
        ViewBag.Timestamp = DateTime.Now;
        
        return View();
    }

    /// <summary>
    /// 演示多個 Filter 的組合使用
    /// </summary>
    [ServiceFilter(typeof(SimpleAuthorizationFilter))]      // Authorization Filter
    [ServiceFilter(typeof(CacheResourceFilter))]            // Resource Filter  
    [ServiceFilter(typeof(ActionTimingFilter))]             // Action Filter
    public IActionResult CombinedFiltersDemo(string data = "combined-demo")
    {
        _logger.LogInformation("CombinedFiltersDemo executed with all filters applied!");
        
        return Json(new { 
            message = "This action demonstrates all filters working together",
            data = data,
            note = "This requires X-API-Key header and will be cached",
            filters = new[] { "Authorization", "Resource", "Action", "Result", "Exception" },
            timestamp = DateTime.Now
        });
    }
}