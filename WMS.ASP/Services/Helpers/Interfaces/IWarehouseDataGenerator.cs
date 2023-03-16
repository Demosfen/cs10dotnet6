using WMS.ASP.Store.Entities;

namespace WMS.ASP.Services.Helpers.Interfaces;

public interface IWarehouseDataGenerator
{
    Task<Box> CreateBoxAsync(Guid paletteId);
    Task<List<Box>> CreateBoxesAsync(Guid paletteId, int n);
    Task<Palette> CreatePaletteAsync(Guid warehouseId);
    Task<List<Palette>> CreatePalettesAsync(Guid warehouseId, int n);
    Task<Warehouse> CreateWarehouse(string warehouseName);
    Task<Palette> CreatePaletteWithBoxesAsync(Guid warehouseId, int nBoxes);
    Task<List<Palette>> CreatePalettesWithBoxesAsync(Guid warehouseId, int nPalettes, int nBoxes);
    Task<Warehouse> CreateWarehouseWithPalettesAndBoxes(string warehouseName , int nPalettes, int nBoxes);
}