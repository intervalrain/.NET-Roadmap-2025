using DvanStore.Application.Contracts.Dtos;

namespace DvanStore.Application.Contracts.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
    Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task<ProductDto?> PatchAsync(int id, PatchProductDto patchProductDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}