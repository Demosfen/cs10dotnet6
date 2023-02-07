using WMS.Store.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WMS.Store.EntityConfigurations;

public sealed class BoxConfigurations : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.ToTable("Boxes");
        
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
        
        builder
            .Property(x => x.ExpiryDate)
            .IsRequired();
    }
}