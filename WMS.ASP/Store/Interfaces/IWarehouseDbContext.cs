using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WMS.ASP.Store.Entities;

namespace WMS.ASP.Store.Interfaces;

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