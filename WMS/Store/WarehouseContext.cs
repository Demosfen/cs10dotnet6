using WMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WMS.Store;

public sealed class WarehouseContext : DbContext
{
    private const string DbFileName = "warehouse.db";

    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<Palette> Palettes => Set<Palette>();

    public DbSet<Box> Boxes => Set<Box>();

    public WarehouseContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=../{DbFileName}")
            .LogTo(Console.WriteLine, LogLevel.Information);

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }*/
}