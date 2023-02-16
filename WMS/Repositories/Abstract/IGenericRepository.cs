using System.Linq.Expressions;

namespace WMS.Repositories.Abstract;

public interface IGenericRepository
    <TEntity> where TEntity : class
{

    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task<TEntity?> GetByIdAsync(Guid id);

    Task InsertAsync(TEntity entity);

    Task DeleteAsync(Guid id);
}