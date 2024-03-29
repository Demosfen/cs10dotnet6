namespace Wms.Web.Store.Entities.Interfaces;

/// <summary>
/// Auditable entity properties: created, updated, deleted
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Date/Time of the entity creation (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Entity date/time update (UTC)
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Entity deleted at (date/time, UTC)
    /// SoftDelete feature
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}