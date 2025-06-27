using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Interfaces;

public interface ICategoryService
{
    Guid Create(CategoryCreateDto dto);
    void Update(Guid id, CategoryCreateDto dto);
    void Delete(Guid id);
    CategoryResponseDto GetById(Guid id);
    IEnumerable<CategoryResponseDto> GetAll();
}