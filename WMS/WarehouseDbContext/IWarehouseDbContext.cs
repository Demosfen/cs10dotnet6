using System.Threading;
using System.Threading.Tasks;
using WMS.WarehouseDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace WMS.WarehouseDbContext;

public interface IWarehouseDbContext
{
    /// <summary>
    /// All the warehouses
    /// </summary>
    DbSet<Warehouse> Warehouses { get; }

    /// <summary>
    /// Saves all the changes in this context to the database
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DatabaseFacade Database { get; }
}