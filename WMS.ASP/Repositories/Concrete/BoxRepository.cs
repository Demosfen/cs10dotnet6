using WMS.ASP.Repositories.Abstract;
using WMS.ASP.Store;
using WMS.ASP.Store.Entities;

namespace WMS.ASP.Repositories.Concrete;

public class BoxRepository : GenericRepository<Box>, IBoxRepository
{
    public BoxRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }
}