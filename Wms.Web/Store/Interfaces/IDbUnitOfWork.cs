namespace Wms.Web.Store.Interfaces;

public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}