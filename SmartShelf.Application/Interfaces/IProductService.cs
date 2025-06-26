using SmartShelf.Application.DTOs;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface IProductService
{
    void Create(ProductCreateDto dto);
    Product? GetById(Guid id);
    IEnumerable<Product> GetAll();
    void Update(Guid id, ProductCreateDto dto);
    void Delete(Guid id);
}