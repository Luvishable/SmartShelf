using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Barcode { get; private set; } = string.Empty;
    public decimal Weight { get; private set; }  // kg cinsinden
    public string Unit { get; private set; } = "kg";

    public Guid CategoryId { get; private set; }
    public Guid SupplierId { get; private set; }

    public decimal PurchasePrice { get; private set; }
    public DateTime EntryDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    private Product() { }

    public Product(
        string name,
        string barcode,
        decimal weight,
        Guid categoryId,
        Guid supplierId,
        decimal purchasePrice,
        string unit,
        DateTime entryDate,
        DateTime? expirationDate)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstNullOrEmpty(barcode, nameof(barcode));
        Guard.AgainstNonPositive(weight, nameof(weight));
        Guard.AgainstNonPositive(purchasePrice, nameof(purchasePrice));
        Guard.AgainstNullOrEmpty(unit, nameof(unit));

        Id = Guid.NewGuid();
        Name = name;
        Barcode = barcode;
        Weight = weight;
        CategoryId = categoryId;
        SupplierId = supplierId;
        PurchasePrice = purchasePrice;
        Unit = unit;
        EntryDate = entryDate;
        ExpirationDate = expirationDate;
    }
    public void Update(
    string name,
    string barcode,
    decimal weight,
    Guid categoryId,
    Guid supplierId,
    decimal purchasePrice,
    string unit,
    DateTime entryDate,
    DateTime? expirationDate)
{
    Guard.AgainstNullOrEmpty(name, nameof(name));
    Guard.AgainstNullOrEmpty(barcode, nameof(barcode));
    Guard.AgainstNonPositive(weight, nameof(weight));
    Guard.AgainstNonPositive(purchasePrice, nameof(purchasePrice));
    Guard.AgainstNullOrEmpty(unit, nameof(unit));

    Name = name;
    Barcode = barcode;
    Weight = weight;
    CategoryId = categoryId;
    SupplierId = supplierId;
    PurchasePrice = purchasePrice;
    Unit = unit;
    EntryDate = entryDate;
    ExpirationDate = expirationDate;
}
}
