namespace SmartShelf.Application.DTOs;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}