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
            .Property(x => x.Id)
            .IsRequired();
        
        builder
            .Property(x => x.Width)
            .IsRequired();

        builder
            .Property(x => x.Height)
            .IsRequired();
        
        builder
            .Property(x => x.Depth)
            .IsRequired();
        
        builder
            .Property(x => x.Volume)
            .IsRequired();
        
        builder
            .Property(x => x.Weight)
            .IsRequired();
    }
}