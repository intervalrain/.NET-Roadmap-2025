using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler(app => app.CustomExceptionHandling());
        // exposed to developers
        // app.UseDeveloperExceptionPage();
    }
    else
    {
        // exposed to end users
        // app.UseExceptionHandler("/Error");
        app.UseExceptionHandler(app => app.CustomExceptionHandling());
        app.UseHsts();
    }

    app.MapGet("Error", () => "Internal server error for testing");
    app.MapGet("test", () =>
    {
        throw new Exception("test exception");
    });
    app.MapGet("helloworld", () => "Hello World!");
    app.Run();
}

public static class IApplicationBuilderExtension
{
    public static IApplicationBuilder CustomExceptionHandling(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerPathFeature?.Error != null)
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "An unexpected error occurred. Please try again."
                });
            }
        });

        return app;
    }
}