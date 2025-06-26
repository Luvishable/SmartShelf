using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface IShelfService
{
    void AddProductToShelf(Guid shelfId, Product product, int quantity);
    Shelf? GetById(Guid id);
    IEnumerable<Shelf> GetAll();
    void RemoveProductFromShelf(Guid shelfId, Guid productId, int quantity);
    void DeactivateShelf(Guid id);
    void ReactivateShelf(Guid id);
}