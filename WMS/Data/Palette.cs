﻿namespace WMS.Data;
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
        (from box in Boxes select box.Volume).Sum()
        + Width * Height * Depth;
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public override decimal Weight => 
        DefaultWeight
        + GetAllBoxes()
            .Sum(box => box.Weight);

    /// <summary>
    /// Palette expiry date computed as
    /// the minimal box exp. date.
    /// </summary>
    public override DateTime? ExpiryDate =>
        (from box in Boxes select box.ExpiryDate).Min();

    /// <summary>
    /// Default palette constructor
    /// </summary>
    /// <param name="width">Palette width</param>
    /// <param name="height">Palette height</param>
    /// <param name="depth">Palette depth</param>
    public Palette(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight = default)
        : base(width, height, depth, weight)
    {
    }

    /// <summary>
    /// Method returning a list of boxes
    /// </summary>
    /// <returns>a List of Boxes</returns>
    public IEnumerable<Box> GetAllBoxes() => Boxes;

    /// <summary>
    /// Output of all information about
    /// boxes on the palette
    /// </summary>
    /// <returns>Palette info</returns>
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