using SmartShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SmartShelf.Application.DTOs;
using SmartShelf.API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// PostgreSQL bağlantısı
builder.Services.AddDbContext<SmartShelfDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDto>();


var app = builder.Build();

// Swagger (OpenAPI)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Gerekli middleware'ler
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization(); // ← ekle
app.MapControllers();   // ← ekle

app.Run();
