using Microsoft.EntityFrameworkCore;
using SmartShelf.Application.Interfaces;
using SmartShelf.Infrastructure.Data;

namespace SmartShelf.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly SmartShelfDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public EfRepository(SmartShelfDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>(); // Hangi entity gönderildiyse onun DbSet'i alınır.
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }
}