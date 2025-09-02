using Microsoft.AspNetCore.Mvc;

using MinimalApi.Models;
using MinimalApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
}
var app = builder.Build();

app.MapGet("/", () => "HelloWorld!");

app.MapGet("/hello", (string? name) => $"Hello, {name ?? "World"}!");

app.AddProductServices();

app.Run();


public static class ProductMinimalApi
{
    public static WebApplication AddProductServices(this WebApplication app)
    {
        app.MapGet("/api/products", async (IProductRepository repo) =>
        {
            return await repo.GetProductListAsync();
        });

        app.MapGet("/api/products/{id:int}", async (int id, IProductRepository repo) =>
        {
            var product = await repo.GetProductByIdAsync(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        }).WithName("GetProductId");

        app.MapPost("/api/products", async ([FromBody]CreateProductDto input, IProductRepository repo) =>
        {
            var product = await repo.CreateProductAsync(input.Name);

            return Results.CreatedAtRoute("GetProductId", new { id = product.Id }, product);
        });

        app.MapPut("/api/products/{id:int}", async (int id, [FromBody]CreateProductDto input, IProductRepository repo) =>
        {
            var result = await repo.UpdateProductAsync(id, input.Name);

            return Results.NoContent();
        });

        app.MapDelete("/api/products/{id:int}", async (int id, IProductRepository repo) =>
        {
            var result = await repo.DeleteProductByIdAsync(id);

            return Results.NoContent();
        });

        return app;
    }
}