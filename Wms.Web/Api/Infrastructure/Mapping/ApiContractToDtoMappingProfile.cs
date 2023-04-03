using AutoMapper;
using JetBrains.Annotations;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Dto;

namespace Wms.Web.Api.Infrastructure.Mapping;

[UsedImplicitly]
public sealed class ApiContractToDtoMappingProfile : Profile
{
    public ApiContractToDtoMappingProfile()
    {
        CreateMap<BoxResponse, BoxDto>()
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("paletteId", opt => opt.MapFrom(src => src.PaletteId))
            .ForCtorParam("width", opt => opt.MapFrom(src => src.Width))
            .ForCtorParam("height", opt => opt.MapFrom(src => src.Height))
            .ForCtorParam("depth", opt => opt.MapFrom(src => src.Depth))
            .ForCtorParam("weight", opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Palette,
                opt => opt.MapFrom(src => src.Palette))
            .ReverseMap();

        CreateMap<PaletteResponse, PaletteDto>()
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("warehouseId", opt => opt.MapFrom(src => src.WarehouseId))
            .ForCtorParam("width", opt => opt.MapFrom(src => src.Width))
            .ForCtorParam("height", opt => opt.MapFrom(src => src.Height))
            .ForCtorParam("depth", opt => opt.MapFrom(src => src.Depth))
            .ForMember(dest => dest.Warehouse, 
                opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.Weight,
                opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Volume,
                opt => opt.MapFrom(src => src.Volume))
            .ForMember(dest => dest.Boxes,
                opt => opt.MapFrom(src => src.Boxes))
            .ReverseMap();
        
        CreateMap<WarehouseResponse, WarehouseDto>()
            .ForCtorParam("id", o => o.MapFrom(src => src.Id))
            .ForCtorParam("name", o => o.MapFrom(src => src.Name))
            .ForMember(dest => dest.Palettes, 
                opt => opt.MapFrom(src => src.Palettes))
            .ReverseMap();
    }
}