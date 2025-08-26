using MyFirstMvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 註冊所有自訂的 Filter 服務
builder.Services.AddScoped<SimpleAuthorizationFilter>();
builder.Services.AddScoped<CacheResourceFilter>();  
builder.Services.AddScoped<ActionTimingFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();
builder.Services.AddScoped<ResponseEnhancementResultFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
