namespace WMS.Data;

/// <summary>
/// A class which describes a warehouse.
/// </summary>
public sealed class Warehouse
{
    /// <summary>
    /// Palettes stored in warehouse
    /// </summary>
    private readonly List <Palette> _palettes = new();

    public IReadOnlyCollection<Palette> Palettes => _palettes;
    
    public override string ToString()
    {
        if (_palettes.Count == 0)
        {
            return $"Warehouse contains no palettes.";
        }
            
        Console.WriteLine($"Warehouse contains {_palettes.Count} palette(s). They are:");
            
        foreach (var palette in _palettes)
        {
            return $"{palette}";   
        }

        var msg = "No palettes found on the warehouse!";
        
        return msg;
    }

    public void AddPalette(Palette palette)
    {
        foreach (var existingPalette in _palettes)
        {
            if (existingPalette.Id == palette.Id)
            {
                Console.WriteLine(
                    $"The palette {palette.Id} already added to the warehouse! Skipping...");
                return;
            }
        }
        
        Console.WriteLine($"The palette {palette.Id} added to the warehouse.");
        _palettes.Add(palette);
    }
    /*
    public void AddPaletteList(List<Palette> palettes)
    {
        foreach (var palette in palettes)
        {
            _palettes.Add(palette);
        }
    }*/

    public void DeletePalette(Guid paletteId)
    {
        var palette = _palettes.SingleOrDefault(x => x.Id == paletteId)
                  ?? throw new InvalidOperationException($"Palette with id = {paletteId} wasn't found");
        
        Console.WriteLine($"Palette with {palette.Id} was removed from the warehouse.");
        
        _palettes.Remove(palette);
        
    }
}