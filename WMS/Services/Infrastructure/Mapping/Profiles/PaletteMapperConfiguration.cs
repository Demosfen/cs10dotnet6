using AutoMapper;
using WMS.Data;
using WMS.Services.Models.Serialization;

namespace WMS.Services.Infrastructure.Mapping.Profiles;

public class PaletteMapperConfiguration: Profile
{
    public PaletteMapperConfiguration()
    {
        CreateMap<PaletteModel, Palette>();
        CreateMap<Palette, PaletteModel>();
    }
}