namespace WMS.Data;
using System.Linq;

/// <summary>
/// A class describing palette
/// </summary>
public sealed class Palette : StorageUnit
{
    /// <summary>
    /// Empty palette weight
    /// </summary>
    private const decimal DefaultWeight = 30;
    
    /// <summary>
    /// Boxes on the palette
    /// </summary>
    public readonly List<Box> Boxes = new();

    /// <summary>
    /// Palette volume computed as
    /// empty palette volume plus
    /// sum of boxes volume
    /// </summary>
    public override decimal Volume =>
        base.Volume
        + Boxes.Sum(box => box.Volume);
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public override decimal Weight => 
        DefaultWeight 
        + Boxes.Sum(box => box.Weight);

    /// <summary>
    /// Palette expiry date computed as
    /// the minimal box exp. date.
    /// </summary>
    public override DateTime? ExpiryDate =>
        Boxes.Min(box => box.ExpiryDate);

    /// <summary>
    /// Default palette constructor
    /// </summary>
    /// <param name="width">Palette width</param>
    /// <param name="height">Palette height</param>
    /// <param name="depth">Palette depth</param>
    /// <param name="weight">Palette weight</param>
    public Palette(
        decimal width,
        decimal height,
        decimal depth)
        : base(width, height, depth, DefaultWeight)
    {
    }

    /// <summary>
    /// Output of all information about
    /// boxes on the palette
    /// </summary>
    /// <returns>Palette info</returns>
    public override string ToString()
    {
        return $"Palette {Id} contains:\n" +
               $"Boxes count: {Boxes.Count}\n" +
               $"Weight: {Weight} kilos\n" +
               $"Volume: {Volume} cubic decimeters\n" +
               $"Exp. Date: {ExpiryDate}.";
    }
}