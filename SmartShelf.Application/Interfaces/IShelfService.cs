using SmartShelf.Domain.Entities;
using SmartShelf.Application.DTOs;
//using SmartShelf.Application.Dtos;

namespace SmartShelf.Application.Interfaces;

public interface IShelfService
{
    void AddProductToShelf(Guid shelfId, Product product, int quantity);
    void RemoveProductFromShelf(Guid shelfId, Guid productId, int quantity);
    void DeactivateShelf(Guid id);
    void ReactivateShelf(Guid id);

    ShelfResponseDto GetById(Guid id);
    IEnumerable<ShelfResponseDto> GetAll();
}