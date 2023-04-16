using AutoMapper;
using JetBrains.Annotations;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Dto;

namespace Wms.Web.Api.Infrastructure.Mapping;

[UsedImplicitly]
public sealed class ApiContractToDtoMappingProfile : Profile
{
    public ApiContractToDtoMappingProfile()
    {
        CreateMap<CreateWarehouseRequest, WarehouseDto>()
            .ForMember(x => x.Id,
                opt => opt.Ignore())
            .ForMember(x => x.IsDeleted,
                opt => opt.Ignore())
            .ForMember(x => x.Palettes,
                opt => opt.Ignore());

        CreateMap<UpdateWarehouseRequest, WarehouseDto>()
            .ForMember(x => x.Id,
                opt => opt.Ignore())
            .ForMember(x => x.IsDeleted,
                opt => opt.Ignore());

        CreateMap<UpdatePaletteRequest, PaletteDto>()
            .ReverseMap();

        CreateMap<CreatePaletteRequest, PaletteDto>()
            .ForMember(x => x.Id,
                opt => opt.Ignore())
            .ForMember(x => x.Weight,
                opt => opt.Ignore())
            .ForMember(x => x.Volume,
                opt => opt.Ignore())
            .ForMember(x => x.ExpiryDate,
                opt => opt.Ignore())
            .ForMember(x => x.Boxes,
                opt => opt.Ignore());

        CreateMap<CreateBoxRequest, BoxDto>()
            .ForMember(x => x.Id, 
                opt => opt.Ignore())
            .ForMember(x => x.Volume, 
                opt => opt.Ignore());
        
        CreateMap<UpdateBoxRequest, BoxDto>()
            .ReverseMap();
    }
}