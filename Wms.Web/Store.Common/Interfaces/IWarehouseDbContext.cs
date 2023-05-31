using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.Store.Common.Interfaces;

public interface IWarehouseDbContext : IDbUnitOfWork, IDisposable
{
    DbSet<Warehouse> Warehouses { get; }
    
    DbSet<Palette> Palettes { get; }
    
    DbSet<Box> Boxes { get; }
    
    DatabaseFacade Database { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}