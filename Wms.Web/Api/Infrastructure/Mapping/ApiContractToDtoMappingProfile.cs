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
        CreateMap<WarehouseRequest, WarehouseDto>()
            .ReverseMap();

        CreateMap<CreateWarehouseRequest, WarehouseDto>()
            .ForMember(x => x.Palettes,
                opt => opt.Ignore())
            .ForMember(x => x.Id,
                opt => opt.Ignore());
    }
}