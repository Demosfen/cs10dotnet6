using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Store;
using Wms.Web.Store.Entities;

namespace Wms.Web.Repositories.Concrete;

public sealed class PaletteRepository : GenericRepository<Palette>, IPaletteRepository
{
    public PaletteRepository(WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }
}