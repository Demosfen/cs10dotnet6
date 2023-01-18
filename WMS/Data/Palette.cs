﻿using System.Text.Json.Serialization;

namespace WMS.Data;

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
    private readonly List<Box> _boxes = new();

    public IReadOnlyCollection<Box> Boxes => _boxes;

    /// <summary>
    /// Palette volume computed as
    /// empty palette volume plus
    /// sum of boxes volume
    /// </summary>
    public override decimal Volume 
        => base.Volume + _boxes.Sum(box => box.Volume);
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public override decimal Weight 
        => DefaultWeight + _boxes.Sum(box => box.Weight);

    /// <summary>
    /// Palette expiry date computed as
    /// the minimal box exp. date.
    /// </summary>
    public override DateTime? ExpiryDate => _boxes.Min(box => box.ExpiryDate);

    /// <summary>
    /// Default palette constructor
    /// </summary>
    /// <param name="width">Palette width</param>
    /// <param name="height">Palette height</param>
    /// <param name="depth">Palette depth</param>
    public Palette(
        decimal width,
        decimal height,
        decimal depth)
        : base(width, height, depth)
    { }

    /// <summary>
    /// Output of all information about
    /// boxes on the palette
    /// </summary>
    /// <returns>Palette info</returns>
    public override string ToString()
    {
        return $"Palette {Id} contains:\n" +
               $"Boxes count: {_boxes.Count}\n" +
               $"Weight: {Weight} kilos\n" +
               $"Volume: {Volume} cubic decimeters\n" +
               $"Exp. Date: {ExpiryDate}.\n";
    }

    public void AddBox(Box box)
    {
        if (box.Width > Width)
        {
            throw new ArgumentException(
                "Width of the box shouldn't be greater than palette.");
        }

        if (box.Depth > Depth)
        {
            throw new ArgumentException(
                "Depth of the box shouldn't be greater than palette.");
        }

        foreach (var existingBox in _boxes)
        {
            if (existingBox.Id == box.Id)
            {
                Console.WriteLine(
                    $"The box {box.Id} already added to the palette{Id}! Skipping...");
                return;
            }
        }

        Console.WriteLine($"The box {box.Id} added to the palette {Id}.");
        _boxes.Add(box);
    }

    public void DeleteBox(Guid boxId)
    {
        var box = _boxes.SingleOrDefault(x => x.Id == boxId)
                  ?? throw new InvalidOperationException($"Box with id = {boxId} wasn't found");

        _boxes.Remove(box);
    }
}