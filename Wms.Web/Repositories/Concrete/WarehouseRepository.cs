using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Store;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Repositories.Concrete;

public sealed class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    private IWarehouseDbContext _dbContext { get; set; }
    
    public WarehouseRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}