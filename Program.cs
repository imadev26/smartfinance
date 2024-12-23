using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Data;
using SmartFinanceAPI.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add URL configuration
builder.WebHost.UseUrls("http://localhost:5062", "https://localhost:7215");

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<SmartFinanceContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()
    ));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware - Order is important!
app.UseMiddleware<ExceptionMiddleware>();

// Enable CORS first
app.UseCors("AllowAll");

// Swagger middleware - before routing middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartFinance API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Routing and authorization middleware
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
