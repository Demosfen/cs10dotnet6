namespace WMS.Data;

public sealed class Box : StorageUnit
{
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime? productionDate = null, 
        DateTime? expiryDate = null) 
        : base(width, height, depth, weight, productionDate, expiryDate)
    {
        
    }
}