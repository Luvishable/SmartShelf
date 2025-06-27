using System.IO.Compression;
using SmartShelf.Application.DTOs;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly List<Category> _categories = new();

    public Guid Create(CategoryCreateDto dto)
    {
        var category = new Category(dto.Name, dto.Description);
        _categories.Add(category);
        return category.Id;
    }

    public void Update(Guid id, CategoryCreateDto dto)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException("Category not found.");

        category.Update(dto.Name, dto.Description);

    }

    public void Delete(Guid id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException("Category not found.");

        _categories.Remove(category);
    }

    public CategoryResponseDto GetById(Guid id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException("Category not found.");

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public IEnumerable<CategoryResponseDto> GetAll()
    {
        return _categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });
    }
}