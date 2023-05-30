namespace Wms.Web.Store.Entities.Interfaces;

public interface IEntityWithId
{
    /// <summary>
    /// Unit ID
    /// </summary>
    Guid Id { get; init; }
}