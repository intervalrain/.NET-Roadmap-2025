using DvanStore.Application.Contracts.Dtos;
using DvanStore.Application.Contracts.Services;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace DvanStore.Api.Controllers;

/// <summary>
/// Product Management API Controller
/// </summary>
/// <remarks>
/// Provides complete product CRUD operations, including RESTful API standard methods
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all products list
    /// </summary>
    /// <remarks>
    /// Returns the complete list of all products in the system
    /// </remarks>
    /// <returns>Product list</returns>
    /// <response code="200">Returns the product list successfully</response>
    /// <response code="500">Server internal error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        _logger.LogInformation("Getting all products list");
        var productDtos = await _productService.GetAllAsync();
        return Ok(productDtos);
    }

    /// <summary>
    /// Get a single product by ID
    /// </summary>
    /// <remarks>
    /// Returns the detailed information of the corresponding product based on the provided product ID
    /// </remarks>
    /// <param name="id">Product unique identifier</param>
    /// <returns>Product detailed information</returns>
    /// <response code="200">Returns the product information successfully</response>
    /// <response code="404">Product not found</response>
    /// <response code="400">Invalid product ID</response>
    /// <response code="500">Server internal error</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductDto>> GetById([Required] int id)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);
        
        var productDto = await _productService.GetByIdAsync(id);
        if (productDto == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        return Ok(productDto);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <remarks>
    /// Create a new product record and return the product information after creation
    /// </remarks>
    /// <param name="createProductDto">Data required to create a product</param>
    /// <returns>Product information after successful creation</returns>
    /// <response code="201">Product created successfully</response>
    /// <response code="400">Request data validation failed</response>
    /// <response code="500">Server internal error</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Description = nameof(Create))]
    public async Task<ActionResult<ProductDto>> Create([FromBody, Required] CreateProductDto createProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new product: {ProductName}", createProductDto.Name);
        
        var productDto = await _productService.CreateAsync(createProductDto);
        
        _logger.LogInformation("Product created with ID: {ProductId}", productDto.Id);
        
        return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
    }

    /// <summary>
    /// Full update product information
    /// </summary>
    /// <remarks>
    /// Use PUT method to fully update all information of the specified product
    /// </remarks>
    /// <param name="id">The ID of the product to be updated</param>
    /// <param name="updateProductDto">Full data required to update the product</param>
    /// <returns>No content, only return status code</returns>
    /// <response code="204">Product updated successfully</response>
    /// <response code="400">Request data validation failed or ID mismatch</response>
    /// <response code="404">Product not found</response>
    /// <response code="500">Server internal error</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([Required] int id, [FromBody, Required] UpdateProductDto updateProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating product with ID: {ProductId}", id);

        var updatedProduct = await _productService.UpdateAsync(id, updateProductDto);
        if (updatedProduct == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found for update", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        _logger.LogInformation("Product with ID {ProductId} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Partial update product information
    /// </summary>
    /// <remarks>
    /// Use PATCH method to partially update the information of the specified product, only update the provided fields
    /// </remarks>
    /// <param name="id">The ID of the product to be updated</param>
    /// <param name="patchProductDto">Data required to partially update the product</param>
    /// <returns>Updated product information</returns>
    /// <response code="200">Product partially updated successfully and return the updated product information</response>
    /// <response code="400">Request data validation failed</response>
    /// <response code="404">Product not found</response>
    /// <response code="500">Server internal error</response>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Description = "patch-product")]
    public async Task<IActionResult> PartialUpdate([Required] int id, [FromBody, Required] PatchProductDto patchProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Partially updating product with ID: {ProductId}", id);

        var productDto = await _productService.PatchAsync(id, patchProductDto);
        if (productDto == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found for partial update", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }
        
        _logger.LogInformation("Product with ID {ProductId} partially updated successfully", id);
        return Ok(productDto);
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <remarks>
    /// Delete the specified product record based on the product ID
    /// </remarks>
    /// <param name="id">The ID of the product to be deleted</param>
    /// <returns>No content, only return status code</returns>
    /// <response code="204">Product deleted successfully</response>
    /// <response code="404">Product not found</response>
    /// <response code="400">Invalid product ID</response>
    /// <response code="500">Server internal error</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([Required] int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);

        var deleted = await _productService.DeleteAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
            return NotFound(new { message = $"Product with ID {id} not found" });
        }

        _logger.LogInformation("Product with ID {ProductId} deleted successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Check if the product exists
    /// </summary>
    /// <remarks>
    /// Use HEAD method to check if the product exists based on the product ID, without returning the content body
    /// </remarks>
    /// <param name="id">The ID of the product to be checked</param>
    /// <returns>No content, only return HTTP status code and headers</returns>
    /// <response code="200">Product exists</response>
    /// <response code="404">Product not found</response>
    /// <response code="400">Invalid product ID</response>
    /// <response code="500">Server internal error</response>
    [HttpHead("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Head([Required] int id)
    {
        var exists = await _productService.ExistsAsync(id);
        return exists ? Ok() : NotFound();
    }

    /// <summary>
    /// Get supported HTTP methods
    /// </summary>
    /// <remarks>
    /// Return the list of all HTTP methods supported by this API endpoint
    /// </remarks>
    /// <returns>Supported HTTP methods in the Allow header</returns>
    /// <response code="200">Successfully return supported HTTP methods</response>
    [HttpOptions]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Options()
    {
        Response.Headers.Allow = "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS";
        return Ok();
    }
}