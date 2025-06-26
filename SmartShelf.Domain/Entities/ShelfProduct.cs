using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class ShelfProduct
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid ShelfId { get; private set; }
    public int Quantity { get; private set; }
    public decimal WeightPerItem { get; private set; }
    public DateTime RecordedAt { get; private set; }

    public decimal TotalWeight => Quantity * WeightPerItem;

    private ShelfProduct() { }

    public ShelfProduct(Guid productId, Guid shelfId, int quantity, decimal weightPerItem)
    {
        Guard.AgainstNonPositive(quantity, nameof(quantity));
        Guard.AgainstNonPositive(weightPerItem, nameof(weightPerItem));

        Id = Guid.NewGuid();
        ProductId = productId;
        ShelfId = shelfId;
        Quantity = quantity;
        WeightPerItem = weightPerItem;
        RecordedAt = DateTime.UtcNow;
    }

    public void AddQuantity(int amount)
    {
        Guard.AgainstNonPositive(amount, nameof(amount));
        Quantity += amount;
    }

    public void RemoveQuantity(int amount)
    {
        Guard.AgainstNonPositive(amount, nameof(amount));

        if (amount > Quantity)
            throw new InvalidOperationException("Cannot remove more than current quantity.");

        Quantity -= amount;
    }
}
