using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }

    public Category(string name, string? description = null)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public void Update(string name, string? description = null)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description;
    }
}
