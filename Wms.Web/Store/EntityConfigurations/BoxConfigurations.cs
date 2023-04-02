using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Entities;

namespace Wms.Web.Store.EntityConfigurations;

public sealed class BoxConfigurations : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.HasKey(x => x.Id);

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
            //.HasConversion<double>()
            .HasConversion<DateTime>()
            .IsRequired();

        builder
            .HasOne(x => x.Palette)
            .WithMany(x => x.Boxes)
            .HasForeignKey(x => x.PaletteId);
    }
}