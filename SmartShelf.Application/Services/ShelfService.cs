using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Domain.Exceptions;

namespace SmartShelf.Application.Services;

public class ShelfService : IShelfService
{
    private readonly List<Shelf> _shelves;
    private readonly List<Product> _products;

    public ShelfService(List<Shelf> shelves, List<Product> products)
    {
        _shelves = shelves;
        _products = products;
    }

    public void AddProductToShelf(Guid shelfId, Product product, int quantity)
    {
        var shelf = _shelves.FirstOrDefault(s => s.Id == shelfId)
            ?? throw new InvalidOperationException("Shelf not found.");

        try
        {
            shelf.AddProduct(product, quantity);
        }
        catch (ShelfOverloadedException ex)
        {
            throw new InvalidOperationException(
                $"Cannot add product. Shelf {ex.ShelfCode} overloaded. Remaining capacity {ex.RemainingCapacity}. You can add {ex.MaxFittableQuantity}"
            );
        }
    }

    public void RemoveProductFromShelf(Guid shelfId, Guid productId, int quantity)
    {
        var shelf = _shelves.FirstOrDefault(s => s.Id == shelfId)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.RemoveProduct(productId, quantity);
    }

    public void DeactivateShelf(Guid id)
    {
        var shelf = _shelves.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.Deactivate();
    }

    public void ReactivateShelf(Guid id)
    {
        var shelf = _shelves.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.Reactivate();
    }

    public ShelfResponseDto GetById(Guid id)
    {
        var shelf = _shelves.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Shelf not found.");

        var productDtos = shelf.Products.Select(p =>
        {
            var product = _products.FirstOrDefault(prod => prod.Id == p.ProductId);
            return new ShelfProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = product?.Name ?? "Unknown",
                Barcode = product?.Barcode ?? "Unknown",
                Quantity = p.Quantity,
                WeightPerItem = p.WeightPerItem,
                RecordedAt = p.RecordedAt
            };
        }).ToList();

        return new ShelfResponseDto
        {
            Id = shelf.Id,
            Code = shelf.Code,
            MaxCapacity = shelf.MaxCapacity,
            IsActive = shelf.IsActive,
            Products = productDtos
        };
    }

    public IEnumerable<ShelfResponseDto> GetAll()
    {
        return _shelves.Select(shelf =>
        {
            var productDtos = shelf.Products.Select(p =>
            {
                var product = _products.FirstOrDefault(prod => prod.Id == p.ProductId);
                return new ShelfProductResponseDto
                {
                    ProductId = p.ProductId,
                    ProductName = product?.Name ?? "Unknown",
                    Barcode = product?.Barcode ?? "Unknown",
                    Quantity = p.Quantity,
                    WeightPerItem = p.WeightPerItem,
                    RecordedAt = p.RecordedAt
                };
            }).ToList();

            return new ShelfResponseDto
            {
                Id = shelf.Id,
                Code = shelf.Code,
                MaxCapacity = shelf.MaxCapacity,
                IsActive = shelf.IsActive,
                Products = productDtos
            };
        });
    }
}