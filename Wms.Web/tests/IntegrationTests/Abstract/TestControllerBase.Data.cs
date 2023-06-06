using AutoMapper;
using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.IntegrationTests.Abstract;

public abstract partial class TestControllerBase
{
    protected readonly IMapper Mapper;
    protected async Task<WarehouseDto> GenerateWarehouse(Guid id)
    {
        var entity = await DbContext.Warehouses.AddAsync(
            new Warehouse{Id = id, Name = Guid.NewGuid().ToString()}, CancellationToken.None);

        await DbContext.SaveChangesAsync(CancellationToken.None);
        return Mapper.Map<WarehouseDto>(entity.Entity);
    }

    protected async Task<PaletteDto> GeneratePalette(Guid warehouseId, Guid paletteId)
    {
        var entity = await DbContext.Palettes.AddAsync(
            new Palette
            {
                Id = paletteId,
                WarehouseId = warehouseId,
                Width = 10,
                Height = 10,
                Depth = 10,
                Volume = 1000,
                Weight = 30
            });

        await DbContext.SaveChangesAsync(CancellationToken.None);

        return Mapper.Map<PaletteDto>(entity.Entity);
    }

    protected async Task<BoxDto> GenerateBox(
        Guid paletteId, Guid boxId)
    {
        var entity = await DbContext.Boxes.AddAsync(
            new Box
            {
                Id = boxId,
                PaletteId = paletteId,
                Width = 5,
                Height = 5,
                Depth = 5,
                Weight = 5,
                Volume = 125,
                ProductionDate = new DateTime(2007,1,1, 0,0,0,0, DateTimeKind.Utc),
                ExpiryDate = new DateTime(2008,1,1, 0,0,0,0, DateTimeKind.Utc)
            });
        
        await DbContext.SaveChangesAsync(CancellationToken.None);

        return Mapper.Map<BoxDto>(entity.Entity);
    }

    protected async Task<Warehouse> DeleteWarehouse(Guid id)
    { 
        var entity = await DbContext.Warehouses.FindAsync(id)
                         ?? throw new EntityNotFoundException(id);

        DbContext.Warehouses.Remove(entity);

        await DbContext.SaveChangesAsync(CancellationToken.None);

        return entity;
    }

    protected async Task<Palette> DeletePalette(Guid id)
    {
        var entity = await DbContext.Palettes.FindAsync(id)
            ?? throw new EntityNotFoundException(id);

        DbContext.Palettes.Remove(entity);

        await DbContext.SaveChangesAsync(CancellationToken.None);

        return entity;
    }

    protected async Task<Box> DeleteBox(Guid id)
    {
        var entity = await DbContext.Boxes.FindAsync(id)
            ?? throw new EntityNotFoundException(id);

        DbContext.Boxes.Remove(entity);

        await DbContext.SaveChangesAsync(CancellationToken.None);

        return entity;
    }
}