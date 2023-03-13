using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Store.Entities;

namespace WMS.Store.EntityConfigurations;

public sealed class PaletteConfigurations : IEntityTypeConfiguration<Palette>
{
    public void Configure(EntityTypeBuilder<Palette> builder)
    {
        builder.ToTable("Palettes");

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
            .HasConversion<DateTime>();

        builder
            .HasOne(x => x.Warehouse)
            .WithMany(x => x.Palettes)
            .HasForeignKey(x => x.WarehouseId);

        builder
            .HasMany<Box>(x => x.Boxes)
            .WithOne(x => x.Palette);
    }
}