namespace WMS.Data;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class StorageUnit
{
    /// <summary>
    /// Object id
    /// </summary>
    private readonly string _id;

    /// <summary>
    /// Object width
    /// </summary>
    private readonly decimal _width;

    /// <summary>
    /// Object height
    /// </summary>
    private readonly decimal _height;

    /// <summary>
    /// Object depth
    /// </summary>
    private readonly decimal _depth;

    /// <summary>
    /// Object weight, which can be calculated or set
    /// during runtime
    /// </summary>
    private decimal _weight;

    protected StorageUnit(string id, decimal width, decimal height, decimal depth, decimal weight)
    {
        _id = id ?? throw new ArgumentNullException(nameof(id));
        _width = width;
        _height = height;
        _depth = depth;
        _weight = weight;
    }
};