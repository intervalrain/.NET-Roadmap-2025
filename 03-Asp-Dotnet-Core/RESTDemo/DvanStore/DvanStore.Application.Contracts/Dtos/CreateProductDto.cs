using System.ComponentModel.DataAnnotations;


namespace DvanStore.Application.Contracts.Dtos;

/// <summary>
/// Create product request data transfer object
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Product name
    /// </summary>
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name length must be between 1 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    [StringLength(500, ErrorMessage = "Product description length cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    [Required(ErrorMessage = "Product price is required")]
    [Range(0.01, 99999.99, ErrorMessage = "Product price must be between 0.01 and 99999.99")]
    public decimal Price { get; set; }

    /// <summary>
    /// Product category
    /// </summary>
    [Required(ErrorMessage = "Product category is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Product category length must be between 1 and 50 characters")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Is active, default is true
    /// </summary>
    public bool IsActive { get; set; } = true;
}