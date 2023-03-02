using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WMS.WarehouseDbContext.Entities;

namespace WMS.WarehouseDbContext.Interfaces;

public interface IWarehouseDbContext
{
    /// <summary>
    /// All the warehouses
    /// </summary>
    DbSet<Warehouse> Warehouses { get; }
    
    DbSet<Palette> Palettes { get; }
    
    DbSet<Box> Boxes { get; }

    /// <summary>
    /// Saves all the changes in this context to the database
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
     
    DatabaseFacade Database { get; }
}