namespace WMS.ASP.Store.Interfaces;

public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}