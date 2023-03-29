using AutoMapper;
using WMS.ASP.Services.Models.Serialization;
using WMS.ASP.Store.Entities;

namespace WMS.ASP.Services.Infrastructure.Mapping;

public sealed class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>().ReverseMap();
        
        CreateMap<PaletteModel, Palette>().ReverseMap();
        
        CreateMap<BoxModel, Box>().ReverseMap();
    }
}