using WMS.Common.Exceptions;
using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Interfaces;

namespace WMS.Repositories.Concrete;

public partial class GenericRepository<TEntity> 
    where TEntity : class, IEntityWithId, ISoftDeletable
{

}