using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Entities;
using Wms.Web.Store.EntityExtensions;

namespace Wms.Web.Store.EntityConfigurations;

public sealed class BoxConfigurations : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.ToTable("Boxes");
        
        builder.HasKey(x => x.Id);
        
        // builder.ConfigureEntity();

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
            .HasConversion<DateTime>()
            .IsRequired();
        
        builder
            .Property(x => x.CreatedAt)
            .IsRequired();
        
        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValue(null);
        
        builder
            .Property(x => x.DeletedAt)
            .HasDefaultValue(null);
    }
}