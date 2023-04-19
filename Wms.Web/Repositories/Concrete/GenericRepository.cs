using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Store.Specifications;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Repositories.Concrete;

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

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default)
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

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _dbSet.SingleOrDefaultAsync(x => x.Id == id,
            cancellationToken: cancellationToken);
    
    public async Task<TEntity?> GetByIdAsync(Guid id, string includeProperties, CancellationToken cancellationToken)
    {
        var entities =
            await GetAllAsync(null, null, includeProperties, cancellationToken: cancellationToken);

        return entities.SingleOrDefault(x => x.Id == id);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity entity = await _dbSet.FindAsync(id)
                         ?? throw new EntityNotFoundException(id);

        _dbSet.Remove(entity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}