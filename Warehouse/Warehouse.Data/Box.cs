namespace Warehouse.Data;

public sealed class Box : Object
{
    /// <summary>
    /// A non-nullable box ID.   
    /// </summary>
    private Guid _id = Guid.NewGuid();
    
    /// <summary>
    /// A non-nullable box width.
    /// </summary>
    private decimal _width;

    /// <summary>
    /// A non-nullable box height.
    /// </summary>
    private decimal _height;
    
    /// <summary>
    /// A non-nullable box depth.
    /// </summary>
    private decimal _depth;
    
    /// <summary>
    /// Volume of a box initially calculated as Width * Depth * Height
    /// </summary>
    private decimal _volume;
    
    /// <summary>
    /// A non-nullable box weight.
    /// </summary>
    private decimal _weight;

    /// <summary>
    /// Date of box expiration (= 100 + ProductionDate)
    /// </summary>
    public DateTime ExpiryDate;

    /// <summary>
    /// Date of box production.
    /// </summary>
    public DateTime ProductionDate;
    
    public Box()
    {
        _id = Guid.NewGuid();
        _width = 1;
        _height = 1;
        _depth = 1;
        _volume = _width * _height * _depth;

    }
}