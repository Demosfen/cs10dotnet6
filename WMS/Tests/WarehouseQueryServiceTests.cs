using FluentAssertions;
using WMS.Data;
using WMS.Services.Concrete;
using Xunit;

namespace WMS.Tests;

public class WarehouseQueryServiceTests
{
    private readonly WareHouseQueryService _sut = new();
    
    [Theory(DisplayName = "Group all pallets by expiration date, and sort them in ascending order of expiration date. Sort pallets by weight in each group.")]
    [MemberData(nameof(TestDataHelper.SortTestData), MemberType = typeof(TestDataHelper))]
    public void SortPalettes_ByExpiryAndWeight(Warehouse warehouse, IReadOnlyCollection<Palette> expectedData)
    {
        var expected = expectedData
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

        // Assert
        _sut.SortByExpiryAndWeight(warehouse).Should().BeEquivalentTo(expected);
    }

    [Theory(DisplayName = "Display the top 3 pallets that contain the boxes with the longest shelf life, sorted in ascending order of volume.")] 
    [MemberData(nameof(TestDataHelper.SortTestData), MemberType = typeof(TestDataHelper))]
    public void GetThreePalettes_SortedByExpiryAndVolume(Warehouse warehouse, IReadOnlyCollection<Palette> expectedData)
    {
        var expected = expectedData
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList();

        // Assert
        _sut.ChooseThreePalettesByExpiryAndVolume(warehouse).Should().BeEquivalentTo(expected);
        
    }
}