using Wms.Web.Repositories.Abstract;
using Wms.Web.Store;
using Wms.Web.Store.Entities;

namespace Wms.Web.Repositories.Concrete;

public class BoxRepository : GenericRepository<Box>, IBoxRepository
{
    public BoxRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }
}