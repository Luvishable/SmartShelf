using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Barcode { get; private set; }
    public string Name { get; private set; }
    public int CategoryId { get; private set; }
    public decimal Weight { get; private set; } // in kilograms
    public string Unit { get; private set; }
    public int SupplierId { get; private set; }
    public decimal PurchasePrice { get; private set; }
    public DateTime EntryDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    private Product() { }

    public Product(
        Guid id,
        string barcode,
        string name,
        int categoryId,
        decimal weight,
        string unit,
        int supplierId,
        decimal purchasePrice,
        DateTime entryDate,
        DateTime? expirationDate = null)
    {
        Validate(name, weight, unit, purchasePrice, entryDate, expirationDate);

        Id = id;
        Barcode = barcode;
        Name = name;
        CategoryId = categoryId;
        Weight = weight;
        Unit = unit;
        SupplierId = supplierId;
        PurchasePrice = purchasePrice;
        EntryDate = entryDate;
        ExpirationDate = expirationDate;
    }

    public void UpdateInfo(
        string name,
        int categoryId,
        decimal weight,
        string unit,
        int supplierId,
        decimal purchasePrice,
        DateTime? expirationDate = null)
    {
        Validate(name, weight, unit, purchasePrice, EntryDate, expirationDate);

        Name = name;
        CategoryId = categoryId;
        Weight = weight;
        Unit = unit;
        SupplierId = supplierId;
        PurchasePrice = purchasePrice;
        ExpirationDate = expirationDate;
    }

    private void Validate(
        string name,
        decimal weight,
        string unit,
        decimal purchasePrice,
        DateTime entryDate,
        DateTime? expirationDate)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstNonPositive(weight, nameof(weight));
        Guard.AgainstNullOrEmpty(unit, nameof(unit));
        Guard.AgainstNegative(purchasePrice, nameof(purchasePrice));
        Guard.AgainstFutureDate(entryDate, nameof(entryDate));
        Guard.AgainstEarlierThan(expirationDate, entryDate, nameof(expirationDate));
    }
}
