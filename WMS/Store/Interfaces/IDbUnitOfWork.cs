namespace WMS.Store.Interfaces;

public interface IDbUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}