namespace WMS.Data;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class ObjProps
{
    /// <summary>
    /// Object id
    /// </summary>
    private readonly int _id;

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
    private readonly decimal _weight;

    public int Id
    {
        get => _id;
        init => _id = String.IsNullOrEmpty(_id.ToString())
            ? throw new ArgumentException("Shouldn't be null or empty", nameof(_id))
            : value;
    }
    
    public decimal Width
    {
        get => _width;
        init => _width = value;
    }

    public decimal Height
    {
        get => _height;
        init => _height = value;
    }

    public decimal Depth 
    {
        get => _depth;
        init => _depth = value;
    }
    public decimal Weight
    {
        get => _weight;
        init => _weight = value;
    }
};