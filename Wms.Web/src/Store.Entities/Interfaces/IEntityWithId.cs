namespace Wms.Web.Store.Entities.Interfaces;

/// <summary>
/// Entities containing ID
/// </summary>
public interface IEntityWithId
{
    /// <summary>
    /// Unit ID
    /// </summary>
    Guid Id { get; init; }
}