using AutoMapper;
using DvanStore.Application.Contracts.Dtos;
using DvanStore.Application.Contracts.Services;
using DvanStore.Domain.Entities;
using DvanStore.Domain.Repositories;

namespace DvanStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return _mapper.Map<Product?, ProductDto?>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<CreateProductDto, Product>(createProductDto);
        var createdProduct = await _repository.AddAsync(product);
        return _mapper.Map<Product, ProductDto>(createdProduct);
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return null;
        }

        var product = _mapper.Map<UpdateProductDto, Product>(updateProductDto);
        product.CreatedAt = existingProduct.CreatedAt;
        
        var updatedProduct = await _repository.UpdateAsync(product);
        return _mapper.Map<Product?, ProductDto?>(updatedProduct);
    }

    public async Task<ProductDto?> PatchAsync(int id, PatchProductDto patchProductDto)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return null;
        }

        _mapper.Map<PatchProductDto, Product>(patchProductDto);
        var updatedProduct = await _repository.UpdateAsync(existingProduct);

        return _mapper.Map<Product?, ProductDto?>(updatedProduct);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }
}