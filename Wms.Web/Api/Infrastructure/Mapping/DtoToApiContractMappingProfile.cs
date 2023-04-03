using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Dto;
using JetBrains.Annotations;

namespace Wms.Web.Api.Infrastructure.Mapping;

[UsedImplicitly]
internal sealed class DtoToApiContractMappingProfile : Profile
{
    public DtoToApiContractMappingProfile()
    {
        CreateMap<WarehouseDto, WarehouseResponse>().ReverseMap();

        CreateMap<PaletteDto, PaletteResponse>().ReverseMap();

        CreateMap<BoxDto, BoxResponse>().ReverseMap();
    }
}