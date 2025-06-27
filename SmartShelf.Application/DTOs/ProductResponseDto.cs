namespace SmartShelf.Application.DTOs;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public string Unit { get; set; } = "kg";

    public decimal PurchasePrice { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    public string CategoryName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
}
