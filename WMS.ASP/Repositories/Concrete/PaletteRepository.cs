using WMS.ASP.Common.Exceptions;
using WMS.ASP.Repositories.Abstract;
using WMS.ASP.Store;
using WMS.ASP.Store.Entities;

namespace WMS.ASP.Repositories.Concrete;

public sealed class PaletteRepository : GenericRepository<Palette>, IPaletteRepository
{
    public PaletteRepository(WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }

    /// <inheritdoc cref="IPaletteRepository"/>
    public async Task AddBoxAsync(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken)
    {
        var palette = await GetByIdAsync(paletteId, cancellationToken)
                      ?? throw new EntityNotFoundException(paletteId);
        
        if (box.Width > palette.Width | box.Height > palette.Height | box.Depth > palette.Depth)
        {
            throw new UnitOversizeException(box.Id);
        }

        if (palette.Boxes.Contains(box))
        {
            Console.WriteLine(
                $"The box id={box.Id} already added to the palette id={palette.Id}! Skipping...");
            
            return;
        }

        Console.WriteLine($"The box id={box.Id} added to the palette id={palette.Id}.");
        
        palette.Boxes.Add(box);

        palette.Weight += box.Weight;
        palette.Volume += box.Volume;
        palette.ExpiryDate = palette.Boxes.Min(x => x.ExpiryDate);
        box.PaletteId = palette.Id;
    }

    /// <inheritdoc cref="IPaletteRepository"/>
    public async Task AddBoxesAsync(Guid paletteId, IEnumerable<Box> boxes, CancellationToken cancellationToken)
    {
        foreach (var box in boxes)
        {
            await AddBoxAsync(paletteId, box, cancellationToken);
        }
    }

    /// <inheritdoc cref="IPaletteRepository"/>
    public async Task DeleteBoxAsync(Guid paletteId,
        Box box,
        CancellationToken cancellationToken)
    {
        var palette = await GetByIdAsync(paletteId, cancellationToken)
                      ?? throw new EntityNotFoundException(paletteId);

        Console.WriteLine($"Box with {box.Id} was removed from the warehouse.");
        
        palette.Weight -= box.Weight;
        palette.Volume -= box.Volume;
        
        palette.Boxes.Remove(box);
        
        palette.ExpiryDate = palette.Boxes.Min(x => x.ExpiryDate);
    }
}