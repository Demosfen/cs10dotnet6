using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wms.Web.Store.Entities;

namespace Wms.Web.Store.Interfaces;

public interface IWarehouseDbContext : IDbUnitOfWork
{
    DbSet<Warehouse> Warehouses { get; }
    
    DbSet<Palette> Palettes { get; }
    
    DbSet<Box> Boxes { get; }
    
    DatabaseFacade Database { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}