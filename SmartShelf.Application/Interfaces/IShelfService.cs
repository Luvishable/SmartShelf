using SmartShelf.Domain.Entities;
using SmartShelf.Application.Dtos;

namespace SmartShelf.Application.Interfaces;

public interface IShelfService
{
    ShelfResponseDto CreateShelf(ShelfCreateDto dto);
    void AddProductToShelf(AddProductToShelfDto dto, Product product);
    Shelf? GetById(Guid id);
    IEnumerable<Shelf> GetAll();
    void RemoveProductFromShelf(Guid shelfId, Guid productId, int quantity);
    void DeactivateShelf(Guid id);
    void ReactivateShelf(Guid id);
}