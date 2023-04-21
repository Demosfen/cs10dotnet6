using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.EntityExtensions;

public static class AuditableEntityExtensions
{
    public static void ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditableEntity
    {
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt)
            .IsRequired();
    }
}