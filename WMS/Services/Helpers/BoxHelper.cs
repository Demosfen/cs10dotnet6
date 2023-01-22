using WMS.Data;

namespace WMS.Services.Helpers;

public class BoxHelper
{
    public static Box GetFirstBox { get; } = new Box(
        1, 1, 1, 1,
        new DateTime(2008, 05, 07, 10, 0, 0));

    public static Box GetSecondBox { get; } = new(2, 1, 3, 3,
        new DateTime(2008, 05, 07, 10, 0, 0),
        new DateTime(2009, 2, 2));

    public static Box GetThirdBox { get; } = new(5, 2, 2, 5,
        new DateTime(2008, 05, 07, 10, 0, 0),
        new DateTime(2009, 2, 2));
}