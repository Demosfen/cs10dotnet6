using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Interfaces;

namespace WMS.Repositories.Concrete;

public partial class GenericRepository<TEntity> 
    where TEntity : class, IEntityWithId, ISoftDeletable
{
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