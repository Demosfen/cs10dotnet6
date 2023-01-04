namespace WMS.Data;

public class Box : StorageUnit
{
    public Box
        (decimal width, 
            decimal height, 
            decimal depth, 
            decimal weight) 
        : base(width, height, depth, weight) { }
}