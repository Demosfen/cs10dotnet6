// using AutoFixture;
// using FluentAssertions;
// using WMS.Services.Concrete;
// using Xunit;
//
// using static WMS.Tests.TestDataHelper;
//
// namespace WMS.Tests;
//
// public class WarehouseRepositoryTests
// {
//     [Fact]
//     public async void Repository_ShouldSaveAndReturnWarehouse()
//     {
//         // Arrange
//         var smallPalette = GetPalette(PaletteSample.Palette1X1X1);
//         var mediumPalette = GetPalette(PaletteSample.Palette5X5X5);
//         var largePalette = GetPalette(PaletteSample.Palette10X10X10);
//         
//         Warehouse warehouse = new ();
//
//         PaletteRepository paletteRepository = new();
//
//         WarehouseRepository sut = new();
//         
//         // Act
//         paletteRepository.AddBox(GetPalette(PaletteSample.Palette1X1X1), GetBox(BoxSample.Box1X1X1));
//         
//         paletteRepository.AddBox(mediumPalette, GetBox(BoxSample.Box1X1X1));
//         paletteRepository.AddBox(mediumPalette, GetBox(BoxSample.Box5X5X5));
//         
//         paletteRepository.AddBox(largePalette, GetBox(BoxSample.Box1X1X1));
//         paletteRepository.AddBox(largePalette, GetBox(BoxSample.Box5X5X5));
//         paletteRepository.AddBox(largePalette, GetBox(BoxSample.Box10X10X10));
//         
//         sut.AddPalette(warehouse, smallPalette);
//         sut.AddPalette(warehouse, mediumPalette);
//         sut.AddPalette(warehouse, largePalette);
//
//         await sut.Save(warehouse, JsonFileName).ConfigureAwait(false);
//
//         var result = await sut.Read(JsonFileName).ConfigureAwait(false);
//         
//         // Assert
//         result.Should().BeEquivalentTo(warehouse);
//     }
// }