namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity not empty error
/// </summary>
public class EntityNotEmptyException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Entity not empty exception
    /// </summary>
    /// <param name="id"></param>
    public EntityNotEmptyException() : base("Entities were not found!")
    {
        
    }

    /// <summary>
    /// Entity not empty exception
    /// </summary>
    /// <param name="id"></param>
    public EntityNotEmptyException(Guid id)
        : base($"The entity with id={id} not empty")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_not_empty";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified id not empty";
}