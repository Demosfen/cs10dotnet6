using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using Xunit;

namespace WMS.Tests;

public class WarehouseServiceTests
{
    private readonly Warehouse _sut = new Warehouse();
    
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse_WhenCall()
    {
        // Arrange
        Warehouse _sut = WarehouseHelper.GetWarehouse();
        
        // Act
        WarehouseRepository repository = new();
        
        await repository.Save(_sut, "TestWarehouse.json").ConfigureAwait(false);

        var result = await repository.Read("TestWarehouse.json").ConfigureAwait(false);
        
        // Assert
        result.Should().BeSameAs(_sut);
    }
}