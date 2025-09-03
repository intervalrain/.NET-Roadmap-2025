namespace DvanStore.Application.Contracts.Dtos;

/// <summary>
/// Product response data transfer object
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Product unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last update time
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Is active
    /// </summary>
    public bool IsActive { get; set; }
}