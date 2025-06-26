using SmartShelf.Domain.Common;
using SmartShelf.Domain.Exceptions;

namespace SmartShelf.Domain.Entities;

public class Shelf
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public decimal MaxCapacity { get; private set; } // kg
    public bool IsActive { get; private set; }

    private readonly List<ShelfProduct> _products = new();
    public IReadOnlyCollection<ShelfProduct> Products => _products.AsReadOnly();

    public Shelf(Guid id, string code, decimal maxCapacity)
    {
        Guard.AgainstNullOrEmpty(code, nameof(code));
        Guard.AgainstNonPositive(maxCapacity, nameof(maxCapacity));

        Id = id;
        Code = code;
        MaxCapacity = maxCapacity;
        IsActive = true;
    }

    public void AddProduct(Product product, int quantity)
    {
        Guard.AgainstNull(product, nameof(product));
        Guard.AgainstNonPositive(quantity, nameof(quantity));

        decimal newWeight = quantity * product.Weight;
        decimal currentLoad = _products.Sum(p => p.TotalWeight);
        decimal remaining = MaxCapacity - currentLoad;
        int maxFittable = (int)Math.Floor(remaining / product.Weight);

        if (currentLoad + newWeight > MaxCapacity)
        {
            IsActive = false;
            throw new ShelfOverloadedException(Code, remaining, maxFittable);
        }

        var existing = _products.FirstOrDefault(p => p.ProductId == product.Id);
        if (existing is not null)
        {
            existing.AddQuantity(quantity);
        }
        else
        {
            _products.Add(new ShelfProduct(product.Id, Id, quantity, product.Weight));
        }
    }

    public void RemoveProduct(Guid productId, int quantity)
    {
        Guard.AgainstNonPositive(quantity, nameof(quantity));

        var shelfProduct = _products.FirstOrDefault(p => p.ProductId == productId);
        if (shelfProduct == null)
            throw new InvalidOperationException("Product not found on shelf.");

        if (shelfProduct.Quantity < quantity)
            throw new InvalidOperationException("Not enough quantity to remove.");

        shelfProduct.RemoveQuantity(quantity);

        if (shelfProduct.Quantity == 0)
            _products.Remove(shelfProduct);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Reactivate()
    {
        IsActive = true;
    }
}
