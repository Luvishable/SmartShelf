using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetExpiredProductsAsync();
}