using WMS.Common.Exceptions;
using WMS.Repositories.Abstract;
using WMS.Store.Entities;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public class PaletteRepository : GenericRepository<Palette>, IPaletteRepository
{
    public PaletteRepository(Store.WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }

    /// <inheritdoc cref="IPaletteRepository"/>
    public async Task AddBox(
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
    public async Task AddBoxes(Guid paletteId, IEnumerable<Box> boxes, CancellationToken cancellationToken)
    {
        foreach (var box in boxes)
        {
            await AddBox(paletteId, box, cancellationToken);
        }
    }

    /// <inheritdoc cref="IPaletteRepository"/>
    public async Task DeleteBox(Guid paletteId,
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