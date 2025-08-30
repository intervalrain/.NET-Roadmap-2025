# Chapter 12.1: In-Memory Caching

## 前言

歡迎來到第十二章！在高效能應用程式的開發中，「快取 (Caching)」是一項至關重要的技術。它的核心思想是將那些頻繁存取但不經常變動的資料，暫時儲存在一個讀取速度更快的儲存體中（通常是記憶體），以避免重複執行昂貴的操作（如資料庫查詢、外部 API 呼叫或複雜的計算）。

在 .NET 中，最簡單、最直接的快取方式就是 **記憶體內快取 (In-Memory Caching)**。

## `IMemoryCache`

.NET 提供了一個內建的記憶體內快取解決方案，可以透過 `IMemoryCache` 介面來存取。它是一個執行緒安全的快取，可以直接在應用程式的處理程序記憶體中儲存和檢索物件。

**優點：**
- **速度極快**：因為資料直接儲存在本機記憶體中，沒有任何網路延遲。
- **設定簡單**：無需任何外部依賴，只需在 `Program.cs` 中註冊即可。

**缺點：**
- **不適用於分散式環境**：在負載平衡的多個伺服器（多個實例）環境中，每個實例都有自己獨立的快取。這被稱為「黏性 (Sticky)」快取，它無法在不同實例之間共享，可能導致資料不一致。
- **生命週期短**：應用程式一旦重啟，所有快取資料都會遺失。

## 使用範例

1.  **在 `Program.cs` 中註冊服務**
    ```csharp
    builder.Services.AddMemoryCache();
    ```

2.  **在服務中注入並使用 `IMemoryCache`**
    一個常見的模式是「先取快取，若無則取資料來源，再存入快取」(Cache-Aside Pattern)。

    ```csharp
    public class ProductService
    {
        private readonly IMemoryCache _cache;
        private readonly MyDbContext _dbContext;
        private const string ProductCacheKeyPrefix = "Product_";

        public ProductService(IMemoryCache cache, MyDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            string cacheKey = $"{ProductCacheKeyPrefix}{id}";

            // 嘗試從快取中獲取資料
            if (!_cache.TryGetValue(cacheKey, out Product product))
            {
                // 如果快取中沒有，則從資料庫讀取
                product = await _dbContext.Products.FindAsync(id);

                if (product != null)
                {
                    // 設定快取選項（例如：5分鐘後過期）
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    // 將資料存入快取
                    _cache.Set(cacheKey, product, cacheEntryOptions);
                }
            }
            return product;
        }
    }
    ```

## 快取失效策略

- **絕對過期 (Absolute Expiration)**：`SetAbsoluteExpiration()`。從存入快取的那一刻起，無論是否被存取，都會在指定的時間點過期。適合那些資料在特定時間點一定會變舊的場景（例如：每日報表）。
- **滑動過期 (Sliding Expiration)**：`SetSlidingExpiration()`。如果在指定的時間段內（例如 5 分鐘）沒有被存取，快取就會過期。每次存取都會重設這個計時器。適合那些「熱點」資料，只要持續被存取，就讓它一直留在快取中。

## 結語

`IMemoryCache` 為單體應用程式或不需在多個實例間共享快取的場景，提供了一個非常高效且易於使用的快取解決方案。然而，當你的應用程式走向分散式架構時，你就需要一個更強大的工具來確保所有實例的資料一致性。這就是分散式快取的用武之地。

---

# Chapter 12.2: Distributed Caching with Redis

## 前言

當你的應用程式部署在多個伺服器上時，記憶體內快取就失效了，因為每個伺服器都有自己的一份快取，無法共享。為了解決這個問題，我們需要 **分散式快取 (Distributed Caching)**。它的核心思想是將快取儲存在一個所有應用程式實例都能存取的、獨立的外部服務中。在眾多分散式快取解決方案中，**Redis** 無疑是最受歡迎的選擇。

## 什麼是 Redis？

Redis (Remote Dictionary Server) 是一個開源的、基於記憶體的、高效能的鍵值 (Key-Value) 資料庫。雖然它可以用作資料庫，但它最常見的用途是作為快取和訊息代理。

**為什麼 Redis 如此適合做快取？**
- **速度極快**：資料主要儲存在記憶體中，讀寫效能非常高。
- **豐富的資料結構**：除了簡單的字串，Redis 還支援雜湊 (Hashes)、列表 (Lists)、集合 (Sets)、有序集合 (Sorted Sets) 等多種資料結構，可以滿足複雜的快取需求。
- **持久化**：Redis 可以設定將記憶體中的資料定期或即時地寫入到磁碟，確保在服務重啟後資料不會遺失。
- **高可用性**：支援主從複製 (Replication) 和哨兵 (Sentinel) / 叢集 (Cluster) 機制，可以建構高可用的快取架構。

## .NET 中的分散式快取抽象

.NET 提供了一套通用的分散式快取抽象 `IDistributedCache`，它定義了存取分散式快取的標準 API。這樣，你的應用程式程式碼可以依賴於 `IDistributedCache`，而無需關心底層具體使用的是 Redis、SQL Server 還是其他快取服務。

`IDistributedCache` 的 API 與 `IMemoryCache` 略有不同，它只能儲存 `byte[]`。這意味著在存入物件前需要先將其序列化（例如，轉成 JSON 字串），取出後再反序列化。

## 使用 Redis 作為分散式快取

1.  **安裝 NuGet 套件**
    - `Microsoft.Extensions.Caching.StackExchangeRedis`

2.  **在 `Program.cs` 中設定**
    ```csharp
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "SampleInstance";
    });
    ```
    在 `appsettings.json` 中設定連線字串：
    ```json
    {
      "ConnectionStrings": {
        "Redis": "localhost:6379"
      }
    }
    ```

3.  **在服務中注入並使用 `IDistributedCache`**
    ```csharp
    public class ProductService
    {
        private readonly IDistributedCache _cache;
        // ...

        public async Task<Product> GetProductAsync(int id)
        {
            string cacheKey = $"Product_{id}";
            Product product = null;

            // 嘗試從快取中獲取資料
            var cachedProductString = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProductString))
            {
                // 如果有，則反序列化
                product = JsonSerializer.Deserialize<Product>(cachedProductString);
            }
            else
            {
                // 如果沒有，則從資料庫讀取
                product = await _dbContext.Products.FindAsync(id);

                if (product != null)
                {
                    // 設定快取選項
                    var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    // 序列化並存入快取
                    var productString = JsonSerializer.Serialize(product);
                    await _cache.SetStringAsync(cacheKey, productString, cacheEntryOptions);
                }
            }
            return product;
        }
    }
    ```

## 結語

Redis 是建構高效能、可擴展 .NET 應用的關鍵元件。透過 .NET 的 `IDistributedCache` 抽象，我們可以輕鬆地將 Redis 整合到應用程式中，作為一個強大的分散式快取，從而顯著提升效能並降低後端負載。掌握記憶體內快取和分散式快取的適用場景與使用方法，是每一位 .NET 後端開發人員的必備技能。