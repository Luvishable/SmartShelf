using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Domain.Exceptions;

namespace SmartShelf.Application.Services;

public class ShelfService : IShelfService
{
    private readonly List<Shelf> _shelves = new();

    public void AddProductToShelf(Guid shelfId, Product product, int quantity)
    {
        var shelf = GetById(shelfId);
        if (shelf == null)
            throw new InvalidOperationException("Shelf not found.");

        try
        {
            shelf.AddProduct(product, quantity);
        }
        catch (ShelfOverloadedException ex)
        {
            throw new InvalidOperationException(
                $"Cannot add product. Shelf {ex.ShelfCode} overloaded. Remaining capacity {ex.RemainingCapacity}. You can add {ex.MaxFittableQuantity}"            );
        }
    }

    public Shelf? GetById(Guid id)
    {
        return _shelves.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Shelf> GetAll()
    {
        return _shelves;
    }

    public void RemoveProductFromShelf(Guid shelfId, Guid productId, int quantity)
    {
        var shelf = GetById(shelfId);
        if (shelf == null)
            throw new InvalidOperationException("Shelf not found.");

        shelf.RemoveProduct(productId, quantity);
    }

    public void DeactivateShelf(Guid id)
    {
        var shelf = GetById(id);
        if (shelf == null)
            throw new InvalidOperationException("Shelf not found.");

        shelf.Deactivate();
    }

    public void ReactivateShelf(Guid id)
    {
        var shelf = GetById(id);
        if (shelf == null)
            throw new InvalidOperationException("Shelf not found.");

        shelf.Reactivate();
    }
}
