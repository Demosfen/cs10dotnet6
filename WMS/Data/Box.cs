namespace WMS.Data;

public class Box : StorageUnit
{
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime production = default, 
        DateTime expiryDate = default, 
        int expiryDays = default) 
        : base(width, height, depth, weight, production, expiryDate, expiryDays)
    {
    }
}