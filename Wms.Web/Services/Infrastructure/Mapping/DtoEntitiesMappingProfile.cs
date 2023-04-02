using AutoMapper;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Infrastructure.Mapping;

public sealed class DtoEntitiesMappingProfile: Profile
{
    public DtoEntitiesMappingProfile()
    {
        CreateMap<Warehouse, WarehouseDto>()
            .ForCtorParam("id", o => o.MapFrom(s => s.Id))
            .ReverseMap();
    }
    
}