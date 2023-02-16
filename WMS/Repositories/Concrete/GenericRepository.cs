using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.WarehouseDbContext;

namespace WMS.Repositories.Concrete;

public sealed class GenericRepository<TEntity> where TEntity : class
{
    internal WarehouseDbContext.WarehouseDbContext dbContext;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(WarehouseDbContext.WarehouseDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties.Split(
            new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) 
                => current.Include(includeProperty));

        return orderBy != null 
            ? await orderBy(query).ToListAsync() 
            : await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) => await dbSet.FindAsync(id);

    public async Task InsertAsync(TEntity entity) => await dbSet.AddAsync(entity);

    public async Task DeleteAsync(Guid id)
    {
        TEntity entity = await dbSet.FindAsync(id)
                         ?? throw new InvalidOperationException($"No entity {nameof(entity)} exist");

        dbSet.Remove(entity);
    }
}