namespace WMS.Data;

public class Box : StorageUnit
{
    public Box(string id, decimal width, decimal height, decimal depth, decimal weight) : base(id, width, height, depth, weight)
    {
    }

    public Box() : base(BASE)
    {
        throw new NotImplementedException();
    }
}