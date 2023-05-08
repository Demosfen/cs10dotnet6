using AutoMapper;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Infrastructure.Mapping;

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