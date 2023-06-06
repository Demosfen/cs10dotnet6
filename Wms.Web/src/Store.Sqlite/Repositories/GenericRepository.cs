using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Common.Interfaces;
using Wms.Web.Store.Entities.Interfaces;

namespace Wms.Web.Store.Sqlite.Repositories;

/// <inheritdoc />
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntityWithId, IAuditableEntity
{
    private readonly DbSet<TEntity> _dbSet;
    
    public IDbUnitOfWork UnitOfWork { get; }

    public GenericRepository(IWarehouseDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();

        UnitOfWork = dbContext;
    }

    /// <inheritdoc />
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
            ? await orderBy(query).ToListAsync(cancellationToken)
            : await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _dbSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    
    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        int offset, 
        int size, 
        string includeProperties,
        CancellationToken cancellationToken)
    {
        var entities =
            await GetAllAsync(
                null, q => q.Skip(offset).Take(size).OrderBy(x => x.CreatedAt), 
                includeProperties: includeProperties, cancellationToken: cancellationToken);
        
        return entities.SingleOrDefault(x => x.Id == id);
    }

    /// <inheritdoc />
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity entity = await _dbSet.FindAsync(id)
                         ?? throw new EntityNotFoundException(id);

        _dbSet.Remove(entity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}