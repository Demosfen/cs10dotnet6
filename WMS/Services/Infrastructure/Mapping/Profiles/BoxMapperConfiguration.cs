using AutoMapper;
using WMS.Data;
using WMS.Services.Models.Serialization;

namespace WMS.Services.Infrastructure.Mapping.Profiles;

public class BoxMapperConfiguration : Profile
{
    public BoxMapperConfiguration()
    {
        CreateMap<BoxModel, Box>();
    }
}