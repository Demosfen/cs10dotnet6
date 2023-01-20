using AutoMapper;
using WMS.Data;
using WMS.Services.Models.Serialization;

namespace WMS.Services.Infrastructure.Mapping.Profiles;

public class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>();
        CreateMap<Warehouse, WarehouseModel>();
    }
}