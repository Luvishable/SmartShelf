using SmartShelf.Application.DTOs;

namespace SmartShelf.Application.Interfaces;

public interface IShelfService
{
    Task<Guid> CreateShelfAsync(ShelfCreateDto dto);

    Task<IEnumerable<ShelfResponseDto>> GetAllAsync();

    Task AddProductToShelfAsync(AddProductToShelfDto dto);

    Task RemoveProductFromShelfAsync(RemoveProductFromShelfDto dto);

    Task DeactivateShelfAsync(Guid id);

    Task ReactivateShelfAsync(Guid id);
}
