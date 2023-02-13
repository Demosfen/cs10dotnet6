using WMS.WarehouseDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMS.Store.EntityConfigurations;
using WMS.WarehouseDbContext.EntityConfigurations;

namespace WMS.WarehouseDbContext;

public sealed class WarehouseDbContext : DbContext, IWarehouseDbContext
{
    private const string DbFileName = "warehouse.db";

    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<Palette> Palettes => Set<Palette>();

    public DbSet<Box> Boxes => Set<Box>();

    public void WarehouseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=../../../../{DbFileName}")
            .LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxConfigurations).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaletteConfigurations).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseConfigurations).Assembly);
    }
}