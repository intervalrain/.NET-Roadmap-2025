using DvanStore.Domain.Entities;
using DvanStore.Domain.Repositories;
using System.Collections.Concurrent;

namespace DvanStore.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly ConcurrentDictionary<int, Product> _products = new();
    private static int _nextId = 1;

    static ProductRepository()
    {
        // 初始化一些範例資料
        var sampleProducts = new[]
        {
            new Product
            {
                Id = 1,
                Name = "智慧手機",
                Description = "最新款智慧手機，配備高解析度螢幕",
                Price = 25999m,
                Category = "電子產品",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Product
            {
                Id = 2,
                Name = "筆記型電腦",
                Description = "高效能筆記型電腦，適合工作和娛樂",
                Price = 45999m,
                Category = "電子產品",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Product
            {
                Id = 3,
                Name = "咖啡豆",
                Description = "來自哥倫比亞的精品咖啡豆",
                Price = 680m,
                Category = "食品",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            }
        };

        foreach (var product in sampleProducts)
        {
            _products[product.Id] = product;
        }
        _nextId = 4;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _products.Values.Where(p => p.IsActive).OrderBy(p => p.Id).AsEnumerable();
        return Task.FromResult(products);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        _products.TryGetValue(id, out var product);
        var result = product?.IsActive == true ? product : null;
        return Task.FromResult(result);
    }

    public Task<Product> AddAsync(Product product)
    {
        product.Id = Interlocked.Increment(ref _nextId);
        product.CreatedAt = DateTime.UtcNow;
        product.IsActive = true;
        
        _products[product.Id] = product;
        return Task.FromResult(product);
    }

    public Task<Product?> UpdateAsync(Product product)
    {
        if (!_products.TryGetValue(product.Id, out var existingProduct) || !existingProduct.IsActive)
        {
            return Task.FromResult<Product?>(null);
        }

        product.CreatedAt = existingProduct.CreatedAt; // 保持原始建立時間
        product.UpdatedAt = DateTime.UtcNow;
        product.IsActive = existingProduct.IsActive;
        
        _products[product.Id] = product;
        return Task.FromResult<Product?>(product);
    }

    public Task<bool> DeleteAsync(int id)
    {
        if (!_products.TryGetValue(id, out var product) || !product.IsActive)
        {
            return Task.FromResult(false);
        }

        // 軟刪除：標記為非活動狀態
        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        return Task.FromResult(true);
    }

    public Task<bool> ExistsAsync(int id)
    {
        var exists = _products.TryGetValue(id, out var product) && product.IsActive;
        return Task.FromResult(exists);
    }
}