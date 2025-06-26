using SmartShelf.Application.Dtos;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Common;
using SmartShelf.Domain.Entities;
using SmartShelf.Domain.Exceptions;

namespace SmartShelf.Application.Services;

public class ShelfService : IShelfService
{
    private readonly List<Shelf> _shelves = new();

    public ShelfResponseDto CreateShelf(ShelfCreateDto dto)
    {
        Guard.AgainstNull(dto, nameof(dto));
        Guard.AgainstNullOrEmpty(dto.Code, nameof(dto.Code));
        Guard.AgainstNonPositive(dto.MaxCapacity, nameof(dto.MaxCapacity));

        var shelf = new Shelf(Guid.NewGuid(), dto.Code, dto.MaxCapacity);
        _shelves.Add(shelf);

        return new ShelfResponseDto
    {
        Id = shelf.Id,
        Code = shelf.Code,
        MaxCapacity = shelf.MaxCapacity,
        IsActive = shelf.IsActive
    };
    }

    public void AddProductToShelf(AddProductToShelfDto dto, Product product)
    {

        Guard.AgainstNull(dto, nameof(dto));
        Guard.AgainstNull(product, nameof(product));
        Guard.AgainstNonPositive(dto.Quantity, nameof(dto.Quantity));

        var shelf = GetById(dto.ShelfId);
        if (shelf == null)
        {
            throw new InvalidOperationException("Shelf not found.");
        }

        try
        {
            shelf.AddProduct(product, dto.Quantity);
        }
        catch (ShelfOverloadedException ex)
        {
            throw new InvalidOperationException(
            $"Cannot add product. Shelf {ex.ShelfCode} overloaded. Remaining capacity {ex.RemainingCapacity} kg. You can add {ex.MaxFittableQuantity} more items."
        );
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
