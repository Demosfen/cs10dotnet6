using WMS.Store.Entities;
using WMS.Services.Abstract;

namespace WMS.Services.Concrete;

public class PaletteRepository : IPaletteRepository
{
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
    }

    public void DeleteBox(Box box, Palette palette)
    {
        var boxId = palette.Boxes.SingleOrDefault(x => x.Id == box.Id)
                    ?? throw new InvalidOperationException($"Box with id = {box.Id} wasn't found");

        Console.WriteLine($"Box with {box.Id} was removed from the warehouse.");
        
        palette.Boxes.Remove(box);
    }
}