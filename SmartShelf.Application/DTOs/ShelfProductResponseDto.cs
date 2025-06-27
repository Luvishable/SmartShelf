namespace SmartShelf.Application.DTOs;

public class ShelfProductResponseDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal WeightPerItem { get; set; }
    public decimal TotalWeight => Quantity * WeightPerItem;
    public DateTime RecordedAt { get; set; }
}