namespace SmartShelf.Domain.Exceptions;

public class ShelfOverloadedException : Exception
{
    public string ShelfCode { get; }
    public decimal RemainingCapacity { get; }
    public int MaxFittableQuantity { get; }

    public ShelfOverloadedException(string shelfCode, decimal remainingCapacity, int maxFittableQuantity)
        : base($"Shelf '{shelfCode}' is overloaded. Remaining capacity : {remainingCapacity} kg. " +
                $"You can only add up to {maxFittableQuantity} more items.")
    {
        ShelfCode = shelfCode;
        RemainingCapacity = remainingCapacity;
        MaxFittableQuantity = maxFittableQuantity;
    }
}