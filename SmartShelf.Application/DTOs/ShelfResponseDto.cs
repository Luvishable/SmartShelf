namespace SmartShelf.Application.Dtos;

public class ShelfResponseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal MaxCapacity { get; set; }
    public bool IsActive { get; set; }
}