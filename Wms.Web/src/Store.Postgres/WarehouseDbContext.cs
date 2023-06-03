using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wms.Web.Store.Common.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Wms.Web.Store.Entities.Interfaces;

namespace Wms.Web.Store.Postgres;

internal sealed class WarehouseDbContext : DbContext, IWarehouseDbContext
{
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<Palette> Palettes => Set<Palette>();

    public DbSet<Box> Boxes => Set<Box>();

    public WarehouseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("");
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => SaveChangesInternalAsync(cancellationToken);

    Task IDbUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        => SaveChangesAsync(cancellationToken);
    
    private async Task<int> SaveChangesInternalAsync(CancellationToken cancellationToken)
    {
        ChangeTracker.DetectChanges();

        var markedAsCreated = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added);

        var markedAsModified = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified);

        var markedAsDeleted = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted);
        
        foreach (var item in markedAsCreated)
        {
            if (item.Entity is not IAuditableEntity entity) continue;
            entity.CreatedAt = DateTime.Now.ToUniversalTime();
        }
        
        foreach (var item in markedAsModified)
        {
            if (item.Entity is not IAuditableEntity entity) continue;
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
        }

        foreach (var item in markedAsDeleted)
        {
            if (item.Entity is not IAuditableEntity entity) continue;
            item.State = EntityState.Unchanged;
            entity.DeletedAt = DateTime.Now.ToUniversalTime();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}