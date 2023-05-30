namespace Wms.Web.Store.Sqlite.Interfaces;

public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}