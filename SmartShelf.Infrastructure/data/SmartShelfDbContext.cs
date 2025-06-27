using Microsoft.EntityFrameworkCore;
using SmartShelf.Domain.Entities;

namespace SmartShelf.Infrastructure.Data;

public class SmartShelfDbContext : DbContext
{
    public SmartShelfDbContext(DbContextOptions<SmartShelfDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Shelf> Shelves => Set<Shelf>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<ShelfProduct> ShelfProducts => Set<ShelfProduct>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}