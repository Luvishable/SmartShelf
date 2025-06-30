using Microsoft.EntityFrameworkCore;
using SmartShelf.Application.Interfaces;
using SmartShelf.Domain.Entities;
using SmartShelf.Infrastructure.Data;

namespace SmartShelf.Infrastructure.Repositories;

public class ShelfRepository : EfRepository<Shelf>, IShelfRepository
{
    public ShelfRepository(SmartShelfDbContext dbContext)
        : base(dbContext)
    {
    }
}
