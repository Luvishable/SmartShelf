namespace SmartShelf.Application.DTOs;

public class ProductCreateDto
{ 
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Weight { get; set; }
    public string Unit { get; set; } = "kg";
    public int SupplierId { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
}