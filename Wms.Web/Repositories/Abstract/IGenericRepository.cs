using System.Linq.Expressions;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Repositories.Abstract;

public interface IGenericRepository<TEntity> : IRepository  
    where TEntity : class
{
    public IDbUnitOfWork UnitOfWork { get; }

    public Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}