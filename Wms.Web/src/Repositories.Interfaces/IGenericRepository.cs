using System.Linq.Expressions;

namespace Wms.Web.Repositories.Interfaces;

/// <summary>
/// Generic repository with CRUD operations
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> : IRepository  
    where TEntity : class
{
    /// <summary>
    /// Get all entities with specified filter, query customization and properties
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get entity by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entity by ID with offset, size and properties
    /// </summary>
    /// <param name="id"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="includeProperties"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TEntity?> GetByIdAsync(
        Guid id,
        int offset = 0, 
        int size = 100,
        string includeProperties = "", 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Safely remove entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}