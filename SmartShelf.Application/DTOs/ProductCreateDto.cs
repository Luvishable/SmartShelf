namespace SmartShelf.Application.DTOs;

public class ProductCreateDto
{
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Weight { get; set; }
    public string Unit { get; set; } = "kg";
    public Guid SupplierId { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get; set; }
}
