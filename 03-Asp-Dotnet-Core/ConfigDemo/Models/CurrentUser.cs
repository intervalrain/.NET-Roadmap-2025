using System.ComponentModel.DataAnnotations;

namespace ConfigDemo.Models;

public class CurrentUser
{
    [Required]
    public string Name { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

}