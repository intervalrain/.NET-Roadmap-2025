var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("swagger/v1/swagger.json", "Route Demo API v1");
            c.RoutePrefix = "";
        });
    }
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}