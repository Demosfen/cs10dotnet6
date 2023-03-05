using WMS.Repositories.Abstract;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public class PaletteRepository : GenericRepository<Palette>, IPaletteRepository
{
    public PaletteRepository(Store.WarehouseDbContext dbContext) 
        : base(dbContext)
    {
    }

    /// <summary>
    /// Performs simple addition of the box to the palette
    /// </summary>
    /// <param name="paletteId">Where to put the box</param>
    /// <param name="box">What box to put</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentException">Sizes inconsistency</exception>
    public async Task AddBox(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken)
    {
        var palette = await GetByIdAsync(paletteId, cancellationToken)
                      ?? throw new InvalidOperationException("");
        
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
        box.PaletteId = palette.Id;
    }
}