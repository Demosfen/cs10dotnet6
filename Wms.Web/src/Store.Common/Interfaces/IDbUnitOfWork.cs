namespace Wms.Web.Store.Common.Interfaces;

/// <summary>
/// Unit of work
/// </summary>
public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}