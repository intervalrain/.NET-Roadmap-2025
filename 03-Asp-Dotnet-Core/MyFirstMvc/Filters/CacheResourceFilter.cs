using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace MyFirstMvc.Filters
{
    /// <summary>
    /// Resource Filter 示例 - 简单的响应缓存实现
    /// </summary>
    public class CacheResourceFilter : IResourceFilter
    {
        private static readonly Dictionary<string, CachedResponse> Cache = new();
        private readonly int _cacheDurationInSeconds;

        public CacheResourceFilter(int cacheDurationInSeconds = 30)
        {
            _cacheDurationInSeconds = cacheDurationInSeconds;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            
            Console.WriteLine($"[ResourceFilter] OnResourceExecuting - Checking cache for key: {cacheKey}");
            
            if (Cache.TryGetValue(cacheKey, out var cachedResponse))
            {
                if (cachedResponse.ExpiresAt > DateTime.UtcNow)
                {
                    // 缓存命中，直接返回缓存的响应
                    Console.WriteLine($"[ResourceFilter] Cache hit! Returning cached response.");
                    context.Result = new ContentResult
                    {
                        Content = cachedResponse.Content,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                    return; // 短路执行，不会继续到后续的 Filter 和 Action
                }
                else
                {
                    // 缓存过期，移除旧缓存
                    Cache.Remove(cacheKey);
                    Console.WriteLine($"[ResourceFilter] Cache expired, removed from cache.");
                }
            }
            
            Console.WriteLine($"[ResourceFilter] Cache miss, continuing to Action...");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            
            Console.WriteLine($"[ResourceFilter] OnResourceExecuted - Caching response for key: {cacheKey}");
            
            // 只缓存成功的响应
            if (context.Result is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode;
                if (statusCode == 200)
                {
                    var content = System.Text.Json.JsonSerializer.Serialize(objectResult.Value);
                    Cache[cacheKey] = new CachedResponse
                    {
                        Content = content,
                        ExpiresAt = DateTime.UtcNow.AddSeconds(_cacheDurationInSeconds)
                    };
                    
                    Console.WriteLine($"[ResourceFilter] Response cached for {_cacheDurationInSeconds} seconds.");
                }
                else
                {
                    Console.WriteLine($"[ResourceFilter] Not caching response with status code: {statusCode}");
                }
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Method);
            keyBuilder.Append(":");
            keyBuilder.Append(request.Path);
            
            if (request.QueryString.HasValue)
            {
                keyBuilder.Append(request.QueryString.Value);
            }
            
            return keyBuilder.ToString();
        }

        private class CachedResponse
        {
            public string Content { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
        }
    }
}