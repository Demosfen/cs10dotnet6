using WMS.Repositories.Abstract;
using WMS.Store.Entities;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public class BoxRepository : GenericRepository<Box>, IBoxRepository
{
    public BoxRepository(Store.WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }
}