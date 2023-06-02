using AutoMapper;
using JetBrains.Annotations;
using Wms.Web.Business.Dto;
using Wms.Web.Contracts.Responses;

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