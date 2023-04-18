namespace Wms.Web.Store.Interfaces;

public interface IAuditableEntity
{
    /// <summary>
    /// Date/Time of the entity creation
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Entity date/time update
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Entity deleted at (date/time)
    /// SoftDelete feature
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}