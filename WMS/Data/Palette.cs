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
        Width * Height * Depth 
        + GetAllBoxes()
            .Sum(box => box.Volume);

    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public override decimal Weight => 
        DefaultWeight
        + GetAllBoxes()
            .Sum(box => box.Weight);

    public override DateTime? ExpiryDate =>
        (from box in Boxes select box.ExpiryDate).Min();

    public Palette(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? productionDate = null,
        DateTime? expiryDate = null)
        : base(width, height, depth, weight, productionDate, expiryDate) { }

    /// <summary>
    /// Method returning a list of boxes
    /// </summary>
    /// <returns>a List of Boxes</returns>
    public IEnumerable<Box> GetAllBoxes() => Boxes;

    /// <summary>
    /// Output of all information about
    /// boxes on the palette
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"\n" +
               $"Palette {Id} contains:\n" +
               $"Boxes count: {Boxes.Count}\n" +
               $"Weight: {Weight} kilos\n" +
               $"Volume: {Volume} cubic decimeters\n" +
               $"Exp. Date: {ExpiryDate}.";
    }
}