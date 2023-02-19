using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Interfaces;
using WMS.WarehouseDbContext.Specifications;

namespace WMS.Repositories.Concrete;

public sealed partial class GenericRepository<TEntity> where TEntity : class, IEntityWithId, ISoftDeletable
{
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(WarehouseDbContext.WarehouseDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
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
            ? await orderBy(query).NotDeleted().ToListAsync()
            : await query.NotDeleted().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task InsertAsync(TEntity entity) => await _dbSet.AddAsync(entity);

    public async Task DeleteAsync(Guid id)
    {
        TEntity entity = await _dbSet.FindAsync(id)
                         ?? throw new InvalidOperationException($"No entity {nameof(entity)} exist");

        _dbSet.Remove(entity);
    }
}