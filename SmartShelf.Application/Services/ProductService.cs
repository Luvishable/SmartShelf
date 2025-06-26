using SmartShelf.Application.Dtos;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Domain.Common;

namespace SmartShelf.Application.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();
    private readonly List<Category> _categories;
    private readonly List<Supplier> _suppliers;

    public ProductService(List<Category> categories, List<Supplier> suppliers)
    {
        _categories = categories;
        _suppliers = suppliers;
    }

    public ProductResponseDto Create(ProductCreateDto dto)
    {
        Guard.AgainstNull(dto, nameof(dto));
        Guard.AgainstNullOrEmpty(dto.Name, nameof(dto.Name));
        Guard.AgainstNullOrEmpty(dto.Barcode, nameof(dto.Barcode));
        Guard.AgainstNonPositive(dto.Weight, nameof(dto.Weight));
        Guard.AgainstNonPositive(dto.PurchasePrice, nameof(dto.PurchasePrice));

        var category = _categories.FirstOrDefault(c => c.Id == dto.CategoryId)
            ?? throw new InvalidOperationException("Category not found.");

        var supplier = _suppliers.FirstOrDefault(s => s.Id == dto.SupplierId)
            ?? throw new InvalidOperationException("Supplier not found.");

        var product = new Product(
            name: dto.Name,
            barcode: dto.Barcode,
            weight: dto.Weight,
            categoryId: dto.CategoryId,
            supplierId: dto.SupplierId,
            purchasePrice: dto.PurchasePrice,
            unit: dto.Unit,
            entryDate: dto.EntryDate,
            expirationDate: dto.ExpirationDate
        );

        _products.Add(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Barcode = product.Barcode,
            Weight = product.Weight,
            Unit = product.Unit,
            PurchasePrice = product.PurchasePrice,
            EntryDate = product.EntryDate,
            ExpirationDate = product.ExpirationDate,
            CategoryName = category.Name,
            SupplierName = supplier.Name
        };
    }

    public ProductResponseDto GetById(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id)
            ?? throw new InvalidOperationException("Product not found.");

        var categoryName = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
        var supplierName = _suppliers.FirstOrDefault(s => s.Id == product.SupplierId);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Barcode = product.Barcode,
            Weight = product.Weight,
            Unit = product.Unit,
            PurchasePrice = product.PurchasePrice,
            EntryDate = product.EntryDate,
            ExpirationDate = product.ExpirationDate,
            CategoryName = categoryName?.Name ?? "Unknown",
            SupplierName = supplierName?.Name ?? "Unknown"
        };

    }

    public IEnumerable<ProductResponseDto> GetAll()
{
    var responseList = new List<ProductResponseDto>();

    foreach (var product in _products)
    {
        var category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
        var supplier = _suppliers.FirstOrDefault(s => s.Id == product.SupplierId);

        var dto = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Barcode = product.Barcode,
            Weight = product.Weight,
            Unit = product.Unit,
            PurchasePrice = product.PurchasePrice,
            EntryDate = product.EntryDate,
            ExpirationDate = product.ExpirationDate,
            CategoryName = category?.Name ?? "Unknown",
            SupplierName = supplier?.Name ?? "Unknown"
        };

        responseList.Add(dto);
    }

    return responseList;
}

    public void Update(Guid productId, ProductCreateDto dto)
{
    Guard.AgainstNull(dto, nameof(dto));
    Guard.AgainstNullOrEmpty(dto.Name, nameof(dto.Name));
    Guard.AgainstNullOrEmpty(dto.Barcode, nameof(dto.Barcode));
    Guard.AgainstNonPositive(dto.Weight, nameof(dto.Weight));
    Guard.AgainstNonPositive(dto.PurchasePrice, nameof(dto.PurchasePrice));

    var product = _products.FirstOrDefault(p => p.Id == productId)
        ?? throw new InvalidOperationException("Product not found.");

    var category = _categories.FirstOrDefault(c => c.Id == dto.CategoryId)
        ?? throw new InvalidOperationException("Category not found.");

    var supplier = _suppliers.FirstOrDefault(s => s.Id == dto.SupplierId)
        ?? throw new InvalidOperationException("Supplier not found.");

    // Güncelleme işlemi
    product.Update(
        name: dto.Name,
        barcode: dto.Barcode,
        weight: dto.Weight,
        categoryId: dto.CategoryId,
        supplierId: dto.SupplierId,
        purchasePrice: dto.PurchasePrice,
        unit: dto.Unit,
        entryDate: dto.EntryDate,
        expirationDate: dto.ExpirationDate
    );
}

public void Delete(Guid productId)
{
    var product = _products.FirstOrDefault(p => p.Id == productId)
        ?? throw new InvalidOperationException("Product not found.");

    _products.Remove(product);
}
}
