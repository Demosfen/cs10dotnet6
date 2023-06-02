using AutoMapper;
using JetBrains.Annotations;
using Wms.Web.Api.Contracts;
using Wms.Web.Business.Dto;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Infrastructure.Mapping;

[UsedImplicitly]
public sealed class ApiContractToDtoMappingProfile : Profile
{
    public ApiContractToDtoMappingProfile()
    {
        CreateMap<WarehouseRequest, WarehouseDto>(MemberList.Source);
        CreateMap<CreateWarehouseRequest, WarehouseDto>(MemberList.Source)
            .IncludeMembers(m => m.WarehouseRequest);
        CreateMap<UpdateWarehouseRequest, WarehouseDto>(MemberList.Source)
            .IncludeMembers(m => m.WarehouseRequest);

        CreateMap<PaletteRequest, PaletteDto>(MemberList.Source);
        CreateMap<CreatePaletteRequest, PaletteDto>(MemberList.Source)
            .IncludeMembers(m => m.PaletteRequest);
        CreateMap<UpdatePaletteRequest, PaletteDto>(MemberList.Source)
            .IncludeMembers(m => m.PaletteRequest);

        CreateMap<BoxRequest, BoxDto>(MemberList.Source);
        CreateMap<CreateBoxRequest, BoxDto>(MemberList.Source)
            .IncludeMembers(m => m.BoxRequest);
        CreateMap<UpdateBoxRequest, BoxDto>(MemberList.Source)
            .IncludeMembers(m => m.BoxRequest);
    }
}