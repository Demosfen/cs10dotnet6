using FluentAssertions;
using WMS.Data;
using WMS.Services.Concrete;
using Xunit;
using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class WarehouseQueryServiceTests
{
    private readonly WareHouseQueryService _sut = new();
    
    [Theory, MemberData(nameof(GetTestData))]
    public void Sort_ByExpiryAndWeight_ShouldReturnSortedPalettes(Warehouse warehouse, IReadOnlyCollection<Palette> expectedData)
    {
        var expected = expectedData
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

        // Assert
        _sut.SortByExpiryAndWeight(warehouse).Should().BeEquivalentTo(expected);
    }

    [Theory, MemberData(nameof(GetTestData))]
    public void GetThreePalettes_ByExpiryAndVolume_ShouldReturnSortedPalettes(Warehouse warehouse, IReadOnlyCollection<Palette> expectedData)
    {
        var expected = expectedData
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList();

        // Assert
        _sut.SortByExpiryAndWeight(warehouse).Should().BeEquivalentTo(expected);
        
    }
}