using AutoMapper;
using JetBrains.Annotations;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;

namespace Wms.Web.Api.Infrastructure.Mapping;

[UsedImplicitly]
public sealed class ApiContractToDtoMappingProfile : Profile
{
    public ApiContractToDtoMappingProfile()
    {
        CreateMap<CreateWarehouseRequest, WarehouseDto>(MemberList.Source);

        CreateMap<UpdateWarehouseRequest, WarehouseDto>(MemberList.Source);

        CreateMap<UpdatePaletteRequest, PaletteDto>(MemberList.Source); 

        CreateMap<CreatePaletteRequest, PaletteDto>(MemberList.Source)
            .ForMember(x => x.Id, 
                opt => opt.Ignore())
            .ForMember(x => x.WarehouseId,
                opt => opt.Ignore());
        
        CreateMap<BoxRequest, BoxDto>(MemberList.Source);
        CreateMap<CreateBoxRequest, BoxDto>(MemberList.Source).IncludeMembers(s => s.BoxRequest);
        
        CreateMap<UpdateBoxRequest, BoxDto>()
            .ReverseMap();
    }
}