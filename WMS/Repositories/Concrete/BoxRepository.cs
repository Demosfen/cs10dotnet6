using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public sealed class BoxRepository: IBoxRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public BoxRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Box?> GetAsync(Guid id, CancellationToken ct = default)
        => await _dbContext.Boxes
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(ct);

    public async Task CreateAsync(Box box, CancellationToken ct = default)
    {
        _dbContext.Boxes.Add(box);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Box box, CancellationToken ct = default)
    {
        _dbContext.Boxes.Update(box);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _dbContext.Boxes
                         .FirstOrDefaultAsync(x => x.Id == id)
                     ?? throw new Exception($"No box with id={id} exist");

        _dbContext.Boxes.Remove(entity);
    }

    public void Dispose()
    {
    }
}