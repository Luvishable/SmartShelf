using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Services;

public class ProductService : IProductService
{
   
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository,
                          IRepository<Category> categoryRepository,
                          IRepository<Supplier> supplierRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<Guid> CreateAsync(ProductCreateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId)
                       ?? throw new InvalidOperationException("Category not found.");

        var supplier = await _supplierRepository.GetByIdAsync(dto.SupplierId)
                       ?? throw new InvalidOperationException("Supplier not found.");

        var product = new Product(
                                  dto.Name,
                                  dto.Barcode,
                                  dto.Weight,
                                  category.Id,
                                  supplier.Id,
                                  dto.PurchasePrice,
                                  dto.Unit,
                                  dto.EntryDate,
                                  dto.ExpirationDate
        );

        await _productRepository.AddAsync(product);
        return product.Id;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var categories = await _categoryRepository.GetAllAsync();
        var suppliers = await _supplierRepository.GetAllAsync();

        return products.Select(p =>
        {
            var category = categories.FirstOrDefault(c => c.Id == p.CategoryId);
            var supplier = suppliers.FirstOrDefault(s => s.Id == p.SupplierId);

            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Weight = p.Weight,
                Unit = p.Unit,
                PurchasePrice = p.PurchasePrice,
                EntryDate = p.EntryDate,
                ExpirationDate = p.ExpirationDate,
                CategoryName = category?.Name ?? "Unknown",
                SupplierName = supplier?.Name ?? "Unknown"
            };
        }
        ).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return null;

        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        var supplier = await _supplierRepository.GetByIdAsync(product.SupplierId);

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
            CategoryName = category?.Name ?? "Unknown",
            SupplierName = supplier?.Name ?? "Unknown"
        };
    }

    public async Task UpdateAsync(Guid id, ProductCreateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Product not found.");

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId)
            ?? throw new InvalidOperationException("Category not found.");

        var supplier = await _supplierRepository.GetByIdAsync(dto.SupplierId)
            ?? throw new InvalidOperationException("Supplier not found.");

        product.Update(
            dto.Name,
            dto.Barcode,
            dto.Weight,
            category.Id,
            supplier.Id,
            dto.PurchasePrice,
            dto.Unit,
            dto.EntryDate,
            dto.ExpirationDate
        );

        _productRepository.Update(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Product not found");

        _productRepository.Delete(product);
    }

    public async Task<List<ProductResponseDto>> GetExpiredProductsAsync()
{
    var products = await _productRepository.GetExpiredProductsAsync();

    var tasks = products.Select(async p =>
    {
        var category = await _categoryRepository.GetByIdAsync(p.CategoryId);
        var supplier = await _supplierRepository.GetByIdAsync(p.SupplierId);

        return new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Barcode = p.Barcode,
            Weight = p.Weight,
            Unit = p.Unit,
            PurchasePrice = p.PurchasePrice,
            EntryDate = p.EntryDate,
            ExpirationDate = p.ExpirationDate,
            CategoryName = category?.Name ?? "Unknown",
            SupplierName = supplier?.Name ?? "Unknown"
        };
    });

    var result = await Task.WhenAll(tasks);
    
    return result.ToList();
}
}