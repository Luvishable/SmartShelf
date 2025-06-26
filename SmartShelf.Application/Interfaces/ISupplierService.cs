using SmartShelf.Application.DTOs;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Interfaces;

public interface ISupplierService
{
    Guid Create(SupplierCreateDto dto);
    void Update(Guid id, SupplierCreateDto dto);
    void Delete(Guid id);
    SupplierResponseDto GetById(Guid id);
    IEnumerable<SupplierResponseDto> GetAll();
}