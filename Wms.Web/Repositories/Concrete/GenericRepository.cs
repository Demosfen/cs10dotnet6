using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Store.Specifications;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Store;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Repositories.Concrete;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntityWithId, ISoftDeletable
{
    private readonly DbSet<TEntity> _dbSet;
    
    public IDbUnitOfWork UnitOfWork { get; }

    public GenericRepository(WarehouseDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();

        UnitOfWork = dbContext;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        string includeProperties,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties.Split(
            new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) 
                => current.Include(includeProperty));

        return orderBy != null 
            ? await orderBy(query).NotDeleted().ToListAsync(cancellationToken)
            : await query.NotDeleted().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _dbSet.FindAsync(id);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) 
        => await _dbSet.AddAsync(entity, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) 
        => await _dbSet.AddRangeAsync(entities, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity entity = await _dbSet.FindAsync(id)
                         ?? throw new EntityNotFoundException(id);

        _dbSet.Remove(entity);
    }
}