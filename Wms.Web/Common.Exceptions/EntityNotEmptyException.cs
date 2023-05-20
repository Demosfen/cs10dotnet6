namespace Wms.Web.Common.Exceptions;

public class EntityNotEmptyException : DomainException
{
    public Guid Id { get; }

    public EntityNotEmptyException() : base("Entities were not found!")
    {
        
    }

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