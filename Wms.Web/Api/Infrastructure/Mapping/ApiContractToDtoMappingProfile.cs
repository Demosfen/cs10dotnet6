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
        
        // CreateMap<CreateBoxRequest, BoxDto>()
        //     .ForMember(x =>x.Depth, 
        //         c => c.MapFrom(p => p.BoxRequest.Depth))
        //     .ForMember(x =>x.Width, 
        //         c => c.MapFrom(p => p.BoxRequest.Width))
        //     .ForMember(x =>x.Height, 
        //         c => c.MapFrom(p => p.BoxRequest.Height))
        //     .ForMember(x => x.Volume, 
        //         opt => opt.Ignore())
        //     .ForMember(x =>x.Weight, 
        //         c => c.MapFrom(p => p.BoxRequest.Weight))
        //     .ForMember(x =>x.ProductionDate, 
        //         c => c.MapFrom(p => p.BoxRequest.ProductionDate))
        //     .ForMember(x =>x.ExpiryDate, 
        //         c => c.MapFrom(p => p.BoxRequest.ExpiryDate));
        
        CreateMap<BoxRequest, BoxDto>(MemberList.Source);
        CreateMap<CreateBoxRequest, BoxDto>().IncludeMembers(s => s.BoxRequest);
        


        CreateMap<UpdateBoxRequest, BoxDto>()
            .ReverseMap();
    }
}