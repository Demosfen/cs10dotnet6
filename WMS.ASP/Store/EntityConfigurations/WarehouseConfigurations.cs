using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.ASP.Store.Entities;

namespace WMS.ASP.Store.EntityConfigurations;

public sealed class WarehouseConfigurations : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        
        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired();
    }
}