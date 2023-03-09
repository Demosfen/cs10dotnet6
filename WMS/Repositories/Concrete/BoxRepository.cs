using WMS.Repositories.Abstract;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public class BoxRepository : GenericRepository<Box>, IBoxRepository
{
    public BoxRepository(Store.WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }
}