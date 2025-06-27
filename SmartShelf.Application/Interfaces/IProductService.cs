using SmartShelf.Application.DTOs;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface IProductService
{
    ProductResponseDto Create(ProductCreateDto dto);
    ProductResponseDto GetById(Guid id);
    IEnumerable<ProductResponseDto> GetAll();
    void Update(Guid id, ProductCreateDto dto);
    void Delete(Guid id);
}