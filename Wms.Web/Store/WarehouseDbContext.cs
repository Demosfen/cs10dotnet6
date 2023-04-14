using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store;

public sealed class WarehouseDbContext : DbContext, IWarehouseDbContext
{
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<Palette> Palettes => Set<Palette>();

    public DbSet<Box> Boxes => Set<Box>();

    public WarehouseDbContext(DbContextOptions opts) : base(opts)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<Palette>().HasQueryFilter(p => !p.IsDeleted);
        
        modelBuilder.Entity<Box>().HasQueryFilter(p => !p.IsDeleted);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => SaveChangesInternalAsync(cancellationToken);

    Task IDbUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        => SaveChangesAsync(cancellationToken);
    
    private async Task<int> SaveChangesInternalAsync(CancellationToken cancellationToken)
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

        return await base.SaveChangesAsync(cancellationToken);
    }
}