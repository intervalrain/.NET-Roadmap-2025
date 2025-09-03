using System.Reflection;

using DvanStore.Application.Contracts.Services;
using DvanStore.Application.Mappings;
using DvanStore.Application.Services;
using DvanStore.Common;
using DvanStore.Common.Filters;
using DvanStore.Domain.Repositories;
using DvanStore.Infrastructure.Repositories;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(opts =>
        {
            opts.InvalidModelStateResponseFactory = context => ProblemDetailFactory.ProblemDetailModelState(context);
        });
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DvanStore API",
            Version = "v1",
            Description = MarkdownService.LoadMarkdown("Overview"),
            Contact = new OpenApiContact
            {
                Name = "Rain Hu",
                Email = "intervalrain@gmail.com"
            }
        });

        // Include XML comments
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

        // Enable annotation support for Markdown
        c.EnableAnnotations();
        
        // Add custom operation filter for markdown loading
        c.OperationFilter<MarkdownOperationFilter>();
    });

    builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

    // Register services
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts =>
        {
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "WedsStore API v1");
            opts.RoutePrefix = "";
            opts.DocumentTitle = "DvanStore";
        });
    }
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}