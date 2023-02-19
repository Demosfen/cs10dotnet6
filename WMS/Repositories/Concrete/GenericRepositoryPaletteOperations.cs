using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Interfaces;

namespace WMS.Repositories.Concrete;

public sealed partial class GenericRepository<TEntity> where TEntity : class, IEntityWithId, ISoftDeletable
{
    /// <summary>
    /// Performs simple addition of the box to the palette
    /// </summary>
    /// <param name="palette">Where to put the box</param>
    /// <param name="box">What box to put</param>
    /// <exception cref="ArgumentException">Sizes inconsistency</exception>
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
        box.PaletteId = palette.Id;
    }

    /// <summary>
    /// Simple deletion of the box from the palette
    /// </summary>
    /// <param name="palette">Where to remove</param>
    /// <param name="box">What to remove</param>
    /// <exception cref="InvalidOperationException">Nothing to remove</exception>
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