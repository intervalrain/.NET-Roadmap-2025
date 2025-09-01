using DIDemo.Interfaces;
using DIDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
// builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/Auth/hash"))
    {
        var password = context.Request.Query["password"].FirstOrDefault() ?? "test123";
        var passwordHasher = context.RequestServices.GetRequiredService<IPasswordHasher>();
        var (hash, salt) = passwordHasher.HashPassword(password);
        
        Console.WriteLine($"=== Hash API Middleware ===");
        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hash: {hash}");
        Console.WriteLine($"Salt: {salt}");
        Console.WriteLine($"===========================");
    }
    
    await next(context);
});

app.Run();
