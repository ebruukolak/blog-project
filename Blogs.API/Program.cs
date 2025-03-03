using Blogs.API.Middleware;
using Blogs.Application;
using Blogs.Application.AppSettingsModels;
using Blogs.Application.Database;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddDatabase(config["Database:ConnectionString"]!);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseMiddleware<ExceptionMappingMiddleware>();
app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();
app.Run();


