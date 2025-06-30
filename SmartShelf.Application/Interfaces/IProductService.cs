using SmartShelf.Application.DTOs;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface IProductService
{
    Task<Guid> CreateAsync(ProductCreateDto dto);
    Task UpdateAsync(Guid id, ProductCreateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto> GetByIdAsync(Guid id); 
    Task<List<ProductResponseDto>> GetExpiredProductsAsync();
}