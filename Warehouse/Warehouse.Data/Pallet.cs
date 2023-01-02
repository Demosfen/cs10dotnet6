using System.Text.Json.Serialization.Metadata;

namespace Warehouse.Data;

public sealed class Pallet : Object
{
    /// <summary>
    /// A non-nullable pallet ID.   
    /// </summary>
    private Guid _id;
    
    /// <summary>
    /// A non-nullable pallet width.
    /// </summary>
    private decimal _width;

    /// <summary>
    /// A non-nullable pallet height.
    /// </summary>
    private decimal _height;
    
    /// <summary>
    /// A non-nullable pallet depth.
    /// </summary>
    private decimal _depth;
    
    /// <summary>
    /// A non-nullable pallet weight.
    /// </summary>
    private decimal _weight = 30;

    /// <summary>
    /// List of boxes on the pallet.
    /// </summary>
    public List<Box> Boxes = new ();

    /// <summary>
    /// Volume of pallet initially calculated as Width * Depth * Height
    /// </summary>
    private decimal _volume;
    
    /// <summary>
    /// Date of pallet expiration (= 100 days + ProductionDate)
    /// </summary>
    public DateTime? ExpiryDate;

    public Pallet()
    {
        _id = Guid.NewGuid();
        _width = 2;
        _height = 2;
        _depth = 2;
        _volume = _width * _height * _depth;

    }
    
}