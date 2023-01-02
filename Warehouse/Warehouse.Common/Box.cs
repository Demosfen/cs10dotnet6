namespace Warehouse.Common;

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
    
    
}