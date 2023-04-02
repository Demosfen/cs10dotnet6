using AutoMapper;
using Wms.Web.Services.Models.Serialization;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Infrastructure.Mapping;

public sealed class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>().ReverseMap();
        
        CreateMap<PaletteModel, Palette>().ReverseMap();
        
        CreateMap<BoxModel, Box>().ReverseMap();
    }
}