namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity not found error
/// </summary>
public class EntityNotFoundException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Entity not found exception
    /// </summary>
    /// <param name="id"></param>
    public EntityNotFoundException() : base("Entities were not found!")
    {
        
    }

    /// <summary>
    /// Entity not found exception
    /// </summary>
    /// <param name="id"></param>
    public EntityNotFoundException(Guid id)
        : base($"The entity with id={id} was not found")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_not_found";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified id was not found";
}