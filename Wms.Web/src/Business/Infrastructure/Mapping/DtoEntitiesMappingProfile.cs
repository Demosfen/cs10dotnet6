using AutoMapper;
using Wms.Web.Business.Dto;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.Business.Infrastructure.Mapping;

public sealed class DtoEntitiesMappingProfile : Profile
{
    public DtoEntitiesMappingProfile()
    {
        CreateMap<Box, BoxDto>()
            .ReverseMap();

        CreateMap<Palette, PaletteDto>()
            .ReverseMap();

        CreateMap<Warehouse, WarehouseDto>()
            .ReverseMap();
    }
}