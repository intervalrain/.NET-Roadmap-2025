using System.Collections.Concurrent;
using System.Security.Cryptography;

using MinimalApi.Models;

namespace MinimalApi.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly ConcurrentDictionary<int, Product> _products = [];
    private static int _increment = 0;

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await Task.CompletedTask;

        if (id > _increment || !_products.ContainsKey(id))
            throw new Exception("Product not found");

        return _products[id];
    }

    public async Task<Product?> GetProductAsync(string name)
    {
        await Task.CompletedTask;

        return _products.Values.FirstOrDefault(p => p.Name == name);
    }

    public async Task<List<Product>> GetProductListAsync()
    {
        await Task.CompletedTask;

        return _products.Values.ToList();
    }

    public async Task<Product> CreateProductAsync(string name)
    {
        await Task.CompletedTask;

        if (_products.Values.Select(p => p.Name).Contains(name))
        {
            throw new Exception($"Product {name} has already existed.");
        }
        
        var product = new Product(Interlocked.Increment(ref _increment), name);
        _products.TryAdd(product.Id, product);

        return product;
    }

    public async Task<bool> DeleteProductAsync(string name)
    {
        await Task.CompletedTask;

        var product = _products.Values.FirstOrDefault(p => p.Name == name);

        if (product == null)
        {
            return false;
        }

        return _products.Remove(product.Id, out _);
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        await Task.CompletedTask;

        if (!_products.ContainsKey(id))
        {
            return false;
        }

        return _products.Remove(id, out _);
    }

    public async Task<Product> UpdateProductAsync(int id, string name)
    {
        await Task.CompletedTask;

        if (!_products.ContainsKey(id))
        {
            throw new Exception($"Product with id: {id} not found.");
        }

        var product = new Product(id, name);
        _products[id] = product;

        return product;
    }
}