using WMS.WarehouseDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMS.Store.EntityConfigurations;
using WMS.WarehouseDbContext.EntityConfigurations;
using WMS.WarehouseDbContext.Interfaces;

namespace WMS.WarehouseDbContext;

public sealed class WarehouseDbContext : DbContext, IWarehouseDbContext
{
    private const string DbFileName = "warehouse.db";

    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<Palette> Palettes => Set<Palette>();

    public DbSet<Box> Boxes => Set<Box>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=../{DbFileName}")
            .LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxConfigurations).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaletteConfigurations).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseConfigurations).Assembly);

        modelBuilder.Entity<Warehouse>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<Palette>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<Box>().HasQueryFilter(m => !m.IsDeleted);
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var markedAsDeleted = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var item in markedAsDeleted)
        {
            if (item.Entity is not ISoftDeletable entity) continue;
            // Set the entity to unchanged
            // (if we mark the whole entity as Modified, every field gets sent to Db as an update)!
            item.State = EntityState.Unchanged;
            // Update only IsDeleted flag
            entity.IsDeleted = true;
        }
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var markedAsDeleted = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var item in markedAsDeleted)
        {
            if (item.Entity is not ISoftDeletable entity) continue;

            item.State = EntityState.Unchanged;

            entity.IsDeleted = true;
        }
        return await base.SaveChangesAsync(ct);
    } 
}