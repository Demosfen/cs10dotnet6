using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.Services.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public sealed class PaletteRepository : IPaletteRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public PaletteRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Palette?> GetAsync(Guid id, CancellationToken ct = default)
        => await _dbContext.Palettes
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken: ct);

    public async Task CreateAsync(Palette palette, CancellationToken ct = default)
    {
        _dbContext.Palettes.Add(palette);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Palette palette, CancellationToken ct)
    {
        _dbContext.Palettes.Update(palette);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _dbContext.Palettes
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct)
                     ?? throw new Exception($"{nameof(Palette)} with {nameof(Palette.Id)}={id} doesn't exist");

        _dbContext.Palettes.Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public void AddBox(Palette palette, Box box)
    {
        
        if (box.Width > palette.Width & box.Height > palette.Height & box.Depth > palette.Depth)
        {
            throw new ArgumentException(
                "Box size (HxWxD) greater than palette!");
        }
        
        if (box.Width > palette.Width)
        {
            throw new ArgumentException(
                "Width of the box shouldn't be greater than palette.");
        }

        if (box.Depth > palette.Depth)
        {
            throw new ArgumentException(
                "Depth of the box shouldn't be greater than palette.");
        }

        if (box.Height > palette.Height)
        {
            throw new ArgumentException("Height of the box shouldn't be greater than palette.");
        }

        if (palette.Boxes.Contains(box))
        {
            Console.WriteLine(
                $"The box {box.Id} already added to the palette{palette.Id}! Skipping...");
            
            return;
        }

        Console.WriteLine($"The box {box.Id} added to the palette {palette.Id}.");
        
        palette.Boxes.Add(box);

        palette.Weight += box.Weight;
        palette.Volume += box.Volume;
        palette.ExpiryDate = palette.Boxes.Min(x => x.ExpiryDate);
    }

    public void DeleteBox(Palette palette, Box box)
    {
        var boxId = palette.Boxes.SingleOrDefault(x => x.Id == box.Id)
                    ?? throw new InvalidOperationException($"Box with id = {box.Id} wasn't found");

        Console.WriteLine($"Box with {box.Id} was removed from the warehouse.");
        
        palette.Weight -= box.Weight;
        palette.Volume -= box.Volume;
        palette.ExpiryDate = palette.Boxes.Min(x => x.ExpiryDate);
        
        palette.Boxes.Remove(box);
    }
}