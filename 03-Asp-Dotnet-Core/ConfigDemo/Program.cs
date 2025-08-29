using ConfigDemo.Models;
using ConfigDemo.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.Configure<UserInfo>(builder.Configuration.GetSection(UserInfo.SectionName));
    builder.Services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();
}

var app = builder.Build();
{
    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}