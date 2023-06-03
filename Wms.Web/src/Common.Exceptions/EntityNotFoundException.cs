namespace Wms.Web.Common.Exceptions;

public class EntityNotFoundException : DomainException
{
    public Guid Id { get; }

    public EntityNotFoundException() : base("Entities were not found!")
    {
        
    }

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