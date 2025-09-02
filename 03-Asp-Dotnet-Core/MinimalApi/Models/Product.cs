namespace MinimalApi.Models;

public record Product(int Id, string Name);

public record CreateProductDto(string Name);