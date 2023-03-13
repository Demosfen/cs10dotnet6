using AutoMapper;
using WMS.Services.Models.Serialization;
using WMS.Store.Entities;

namespace WMS.Services.Infrastructure.Mapping;

public sealed class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>().ReverseMap();
        
        CreateMap<PaletteModel, Palette>().ReverseMap();
        
        CreateMap<BoxModel, Box>().ReverseMap();
    }
}