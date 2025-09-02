using MinimalApi.Models;

namespace MinimalApi.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(string name);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> GetProductAsync(string name);
    Task<List<Product>> GetProductListAsync();
    Task<bool> DeleteProductAsync(string name);
    Task<bool> DeleteProductByIdAsync(int id);
    Task<Product> UpdateProductAsync(int id, string name);
}