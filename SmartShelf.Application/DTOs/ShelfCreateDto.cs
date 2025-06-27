namespace SmartShelf.Application.DTOs;

public class ShelfCreateDto
{
    public string Code { get; set; } = string.Empty;
    public decimal MaxCapacity { get; set; }
}