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

        var msg = $"Warehouse contains {Palettes.Count} palettes:\n";
        
        foreach (var palette in _palettes)
        {
            msg += palette.ToString();
        }

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

    public void DeletePalette(Guid paletteId)
    {
        var palette = _palettes.SingleOrDefault(x => x.Id == paletteId)
                  ?? throw new InvalidOperationException($"Palette with id = {paletteId} wasn't found");
        
        Console.WriteLine($"Palette with {palette.Id} was removed from the warehouse.");
        
        _palettes.Remove(palette);
        
    }

    // public List<Palette> SortByExpiryAndWeight() =>
    //         new (Palettes
    //         .Where(p => p.ExpiryDate.HasValue)
    //         .OrderBy(p => p.ExpiryDate) 
    //         .ThenBy(p => p.Weight));
    //
    // public List<Palette> ChooseThreePalettesByExpiryAndVolume() =>
    //     new (Palettes
    //         .OrderByDescending(p => p.ExpiryDate)
    //         .ThenBy(p => p.Volume)
    //         .Take(3));
}