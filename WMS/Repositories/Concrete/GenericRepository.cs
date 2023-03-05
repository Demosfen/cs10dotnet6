using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.Store.Interfaces;
using WMS.WarehouseDbContext.Interfaces;
using WMS.WarehouseDbContext.Specifications;

namespace WMS.Repositories.Concrete;

public partial class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntityWithId, ISoftDeletable
{
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(Store.WarehouseDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();

        UnitOfWork = dbContext;
    }

    public IDbUnitOfWork UnitOfWork { get; }

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

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken) 
        => await _dbSet.AddAsync(entity, cancellationToken);

    public async Task InsertMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) 
        => await _dbSet.AddRangeAsync(entities, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity entity = await _dbSet.FindAsync(id)
                         ?? throw new InvalidOperationException($"No entity {nameof(TEntity)} with ID = {id} exist");

        _dbSet.Remove(entity);
    }
}