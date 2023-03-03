﻿using AutoMapper;
using WMS.Services.Models.Serialization;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Services.Infrastructure.Mapping;

public class WarehouseMapperConfiguration : Profile
{
    public WarehouseMapperConfiguration()
    {
        CreateMap<WarehouseModel, Warehouse>();
        CreateMap<Warehouse, WarehouseModel>();
        
        CreateMap<PaletteModel, Palette>();
        CreateMap<Palette, PaletteModel>();
        
        CreateMap<BoxModel, Box>();
        CreateMap<Box, BoxModel>();
    }
}