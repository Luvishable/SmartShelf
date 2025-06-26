using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class Supplier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string TaxNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Supplier() { }

    public Supplier(string name, string taxNumber, string email, string phone, string address)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        TaxNumber = taxNumber;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public void Update(string name, string taxNumber, string email, string phone, string address)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));

        Name = name;
        TaxNumber = taxNumber;
        Email = email;
        Phone = phone;
        Address = address;
    }
}
