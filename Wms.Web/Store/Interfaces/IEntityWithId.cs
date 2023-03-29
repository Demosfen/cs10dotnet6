namespace WMS.ASP.Store.Interfaces;

public interface IEntityWithId
{
    /// <summary>
    /// Unit ID
    /// </summary>
    Guid Id { get; init; }
}