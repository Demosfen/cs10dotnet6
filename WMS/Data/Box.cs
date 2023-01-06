namespace WMS.Data;

public class Box : StorageUnit
{
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime production, 
        DateTime expiry) 
        : base(width, height, depth, weight, production, expiry) { }
}