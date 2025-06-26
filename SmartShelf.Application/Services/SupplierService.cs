using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly List<Supplier> _suppliers = new();

    public Guid Create(SupplierCreateDto dto)
    {
        var supplier = new Supplier(dto.Name, dto.TaxNumber, dto.Email, dto.Phone, dto.Address);
        _suppliers.Add(supplier);
        return supplier.Id;
    }

    public void Update(Guid id, SupplierCreateDto dto)
    {
        var supplier = _suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Supplier not found.");

        supplier.Update(dto.Name, dto.TaxNumber, dto.Email, dto.Phone, dto.Address);
    }

    public void Delete(Guid id)
    {
        var supplier = _suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Supplier not found.");

        _suppliers.Remove(supplier);
    }

    public SupplierResponseDto GetById(Guid id)
    {
        var supplier = _suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new InvalidOperationException("Supplier not found.");

        return new SupplierResponseDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            TaxNumber = supplier.TaxNumber,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address
        };
    }

    public IEnumerable<SupplierResponseDto> GetAll()
    {
        return _suppliers.Select(s => new SupplierResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            TaxNumber = s.TaxNumber,
            Email = s.Email,
            Phone = s.Phone,
            Address = s.Address
        });
    }
}
