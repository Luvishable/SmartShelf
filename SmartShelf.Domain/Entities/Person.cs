using SmartShelf.Domain.Common;

namespace SmartShelf.Domain.Entities;

public class Person
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }

    private Person() { }

    public Person(Guid id, string fullName, string role)
    {
        Guard.AgainstNullOrEmpty(fullName, nameof(fullName));
        Guard.AgainstNullOrEmpty(role, nameof(role));

        Id = id;
        FullName = fullName;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;

    public void Reactivate() => IsActive = true;
}
