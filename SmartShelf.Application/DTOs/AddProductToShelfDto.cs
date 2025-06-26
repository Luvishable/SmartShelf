namespace SmartShelf.Application.Dtos;

public class AddProductToShelfDto
{
    public Guid ShelfId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}