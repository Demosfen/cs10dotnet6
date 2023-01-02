namespace Warehouse.Common;

public sealed class Pallet : Object
{
    /// <summary>
    /// A non-nullable pallet ID.   
    /// </summary>
    private Guid _id = Guid.NewGuid();
    
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
    /// Date of pallet expiration (= 100 days + ProductionDate)
    /// </summary>
    public DateTime ExpiryDate;
    
}