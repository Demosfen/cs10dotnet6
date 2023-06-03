namespace Wms.Web.Store.Common.Interfaces;

public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}