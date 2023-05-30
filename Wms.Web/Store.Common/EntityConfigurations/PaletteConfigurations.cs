using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Entities;
using Wms.Web.Store.EntityExtensions;

namespace Wms.Web.Store.EntityConfigurations;

public sealed class PaletteConfigurations : IEntityTypeConfiguration<Palette>
{
    public void Configure(EntityTypeBuilder<Palette> builder)
    {
        builder.ToTable("Palettes");

        builder.HasKey(x => x.Id);
        
        builder.ConfigureAuditableEntity();
        
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
            .HasMany<Box>(x => x.Boxes)
            .WithOne()
            .HasForeignKey(x => x.PaletteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}