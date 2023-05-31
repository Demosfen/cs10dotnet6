using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Common.EntityExtensions;
using Wms.Web.Store.Entities;

namespace Wms.Web.Store.Common.EntityConfigurations;

public sealed class WarehouseConfigurations : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(x => x.Id);
        
        builder.ConfigureAuditableEntity();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder.HasIndex(e => e.Name).IsUnique();

        builder 
            .HasMany<Palette>(x => x.Palettes)
            .WithOne()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}