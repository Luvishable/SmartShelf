using Microsoft.EntityFrameworkCore;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Infrastructure.Data;

namespace SmartShelf.Infrastructure.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(SmartShelfDbContext dbContext) : base(dbContext) { }

    public async Task<List<Product>> GetExpiredProductsAsync()
    {
        return await _dbContext.Products
            .Where(p => p.ExpirationDate != null && p.ExpirationDate < DateTime.UtcNow)
            .ToListAsync();
    }
    
}