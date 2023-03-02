using WMS.Services;
using WMS.Services.Concrete;

namespace WMS.Tests;

public class WarehouseDbTests: IClassFixture<TestDatabaseFixture>
{
    private TestDatabaseFixture _fixture;

    public WarehouseDbTests(TestDatabaseFixture fixture) => this._fixture = fixture;
    

    [Fact]
    public void Test1()
    {
        var context = _fixture.CreateContext();
        
    }
}