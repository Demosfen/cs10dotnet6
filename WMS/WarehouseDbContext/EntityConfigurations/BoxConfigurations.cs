using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.WarehouseDbContext.Entities;

namespace WMS.WarehouseDbContext.EntityConfigurations;

public sealed class BoxConfigurations : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .IsRequired();
        
        builder
            .Property(x => x.Width)
            .HasConversion<double>()
            .IsRequired();

        builder
            .Property(x => x.Height)
            .HasConversion<double>()
            .IsRequired();
        
        builder
            .Property(x => x.Depth)
            .HasConversion<double>()
            .IsRequired();
        
        builder
            .Property(x => x.Volume)
            .HasConversion<double>()
            .IsRequired();
        
        builder
            .Property(x => x.Weight)
            .HasConversion<double>()
            .IsRequired();
        
        builder
            .Property(x => x.ExpiryDate)
            .HasConversion<double>()
            .IsRequired();

        builder
            .Property(x => x.ExpiryDate)
            .HasConversion<DateTime>();

        builder
            .Property(x => x.PaletteId)
            .ValueGeneratedNever()
            .HasDefaultValue(null);
    }
}