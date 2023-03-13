using WMS.Repositories.Abstract;
using WMS.Store;
using WMS.Store.Entities;

namespace WMS.Repositories.Concrete;

public class BoxRepository : GenericRepository<Box>, IBoxRepository
{
    public BoxRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }
}