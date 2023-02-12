using AutoMapper;
using WMS.WarehouseDbContext.Entities;
using WMS.Services.Models.Serialization;

namespace WMS.Services.Infrastructure.Mapping.Profiles;

public class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>();
        CreateMap<Warehouse, WarehouseModel>();
        
        CreateMap<PaletteModel, Palette>();
        CreateMap<Palette, PaletteModel>();
        
        CreateMap<BoxModel, Box>();
        CreateMap<Box, BoxModel>();
    }
}