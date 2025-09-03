using System.ComponentModel.DataAnnotations;


namespace DvanStore.Application.Contracts.Dtos;

/// <summary>
/// Partial update product request data transfer object
/// </summary>
public class PatchProductDto
{
    /// <summary>
    /// Product name (optional)
    /// </summary>
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name length must be between 1 and 100 characters")]
    public string? Name { get; set; }

    /// <summary>
    /// Product description (optional)
    /// </summary>
    [StringLength(500, ErrorMessage = "Product description length cannot exceed 500 characters")]
    public string? Description { get; set; }

    /// <summary>
    /// Product price (optional)
    /// </summary>
    [Range(0.01, 99999.99, ErrorMessage = "Product price must be between 0.01 and 99999.99")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Product category (optional)
    /// </summary>
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Product category length must be between 1 and 50 characters")]
    public string? Category { get; set; }

    /// <summary>
    /// Is active (optional)
    /// </summary>
    public bool? IsActive { get; set; }
}