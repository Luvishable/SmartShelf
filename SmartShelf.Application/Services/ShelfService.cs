using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Domain.Exceptions;

namespace SmartShelf.Application.Services;

public class ShelfService : IShelfService
{
    private readonly IShelfRepository _shelfRepository;
    private readonly IProductRepository _productRepository;

    public ShelfService(IShelfRepository shelfRepository, IProductRepository productRepository)
    {
        _shelfRepository = shelfRepository;
        _productRepository = productRepository;
    }

    public async Task<Guid> CreateShelfAsync(ShelfCreateDto dto)
    {
        var shelf = new Shelf(Guid.NewGuid(), dto.Code, dto.MaxCapacity);
        await _shelfRepository.AddAsync(shelf);
        return shelf.Id;
    }

    public async Task<IEnumerable<ShelfResponseDto>> GetAllAsync()
    {
        var shelves = await _shelfRepository.GetAllAsync();

        var result = new List<ShelfResponseDto>();

        foreach (var shelf in shelves)
        {
            var productDtos = new List<ShelfProductResponseDto>();

            foreach (var p in shelf.Products)
            {
                var product = await _productRepository.GetByIdAsync(p.ProductId);
                productDtos.Add(new ShelfProductResponseDto
                {
                    ProductId = p.ProductId,
                    ProductName = product?.Name ?? "Unknown",
                    Barcode = product?.Barcode ?? "Unknown",
                    Quantity = p.Quantity,
                    WeightPerItem = p.WeightPerItem,
                    RecordedAt = p.RecordedAt
                });
            }

            result.Add(new ShelfResponseDto
            {
                Id = shelf.Id,
                Code = shelf.Code,
                MaxCapacity = shelf.MaxCapacity,
                IsActive = shelf.IsActive,
                Products = productDtos
            });
        }

        return result;
    }

    public async Task AddProductToShelfAsync(AddProductToShelfDto dto)
    {
        var shelf = await _shelfRepository.GetByIdAsync(dto.ShelfId)
            ?? throw new InvalidOperationException("Shelf not found.");

        var product = await _productRepository.GetByIdAsync(dto.ProductId)
            ?? throw new InvalidOperationException("Product not found.");

        try
        {
            shelf.AddProduct(product, dto.Quantity);
            _shelfRepository.Update(shelf);
        }
        catch (ShelfOverloadedException ex)
        {
            throw new InvalidOperationException(
                $"Cannot add product. Shelf {ex.ShelfCode} overloaded. Remaining capacity {ex.RemainingCapacity}. You can add {ex.MaxFittableQuantity}");
        }
    }

    public async Task RemoveProductFromShelfAsync(RemoveProductFromShelfDto dto)
    {
        var shelf = await _shelfRepository.GetByIdAsync(dto.ShelfId)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.RemoveProduct(dto.ProductId, dto.Quantity);
        _shelfRepository.Update(shelf);
    }

    public async Task DeactivateShelfAsync(Guid id)
    {
        var shelf = await _shelfRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.Deactivate();
        _shelfRepository.Update(shelf);
    }

    public async Task ReactivateShelfAsync(Guid id)
    {
        var shelf = await _shelfRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Shelf not found.");

        shelf.Reactivate();
        _shelfRepository.Update(shelf);
    }
}
