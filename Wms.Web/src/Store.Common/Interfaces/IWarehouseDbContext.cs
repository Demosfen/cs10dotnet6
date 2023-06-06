using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.Store.Common.Interfaces;

/// <summary>
/// Wms database context
/// </summary>
public interface IWarehouseDbContext : IDbUnitOfWork, IDisposable
{
    /// <summary>
    /// Set warehouses
    /// </summary>
    DbSet<Warehouse> Warehouses { get; }
    
    /// <summary>
    /// Set palettes
    /// </summary>
    DbSet<Palette> Palettes { get; }
    
    /// <summary>
    /// Set boxes
    /// </summary>
    DbSet<Box> Boxes { get; }
    
    DatabaseFacade Database { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}