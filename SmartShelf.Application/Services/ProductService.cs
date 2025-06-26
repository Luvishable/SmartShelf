using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Common;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Applicaiton.Services;

public class ProductService : IProductService
{
    // Fake in-memory storage for demonstration purposes
    private readonly List<Product> _products = new();

    public void Create(ProductCreateDto dto)
    {
        Guard.AgainstNullOrEmpty(dto.Name, nameof(dto.Name));
        Guard.AgainstNonPositive(dto.Weight, nameof(dto.Weight));
        Guard.AgainstFutureDate(dto.EntryDate, nameof(dto.EntryDate));

        var product = new Product(
            id: Guid.NewGuid(),
            barcode: dto.Barcode,
            name: dto.Name,
            categoryId: dto.CategoryId,
            weight: dto.Weight,
            unit: dto.Unit,
            supplierId: dto.SupplierId,
            purchasePrice: dto.PurchasePrice,
            entryDate: dto.EntryDate,
            expirationDate: dto.ExpirationDate
        );

        _products.Add(product);
    }

    public Product? GetById(Guid id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.AsReadOnly();
    }

    public void Update(Guid id, ProductCreateDto dto)
    {
        var product = GetById(id);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.UpdateInfo(
            name: dto.Name,
            categoryId: dto.CategoryId,
            weight: dto.Weight,
            unit: dto.Unit,
            supplierId: dto.SupplierId,
            purchasePrice: dto.PurchasePrice,
            expirationDate: dto.ExpirationDate
        );
    }

    public void Delete(Guid id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _products.Remove(product);
        }
        else
        {
            throw new InvalidOperationException("Product not found.");
        }
    }
}