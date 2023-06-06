namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity conflict error: entity was deleted
/// </summary>
public class EntityWasDeletedException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Entity was deleted exception
    /// </summary>
    /// <param name="id"></param>
    public EntityWasDeletedException() : base("Entity was deleted.")
    {
        
    }

    /// <summary>
    /// Entity was deleted exception
    /// </summary>
    /// <param name="id"></param>
    public EntityWasDeletedException(Guid id)
        : base($"The entity with id={id} was deleted.")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_deleted";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified id was deleted";
}