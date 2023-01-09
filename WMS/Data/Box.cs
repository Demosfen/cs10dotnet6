namespace WMS.Data;

public class Box : StorageUnit
{
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime? productionDate = null, 
        DateTime? expiryDate = null, 
        double expiryDays = 100) 
        : base(width, height, depth, weight, productionDate, expiryDate, expiryDays)
    {
    }
}