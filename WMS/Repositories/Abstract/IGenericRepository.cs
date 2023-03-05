using System.Linq.Expressions;
using WMS.Store.Interfaces;

namespace WMS.Repositories.Abstract;

public interface IGenericRepository<TEntity> 
    where TEntity : class
{
    public IDbUnitOfWork UnitOfWork { get; }

    public Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}