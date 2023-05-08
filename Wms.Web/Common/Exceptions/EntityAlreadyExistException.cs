namespace Wms.Web.Common.Exceptions;

public class EntityAlreadyExistException : DomainException
{
    public Guid Id { get; }

    public EntityAlreadyExistException() : base("Entities were not found!")
    {
        
    }

    public EntityAlreadyExistException(Guid id)
        : base($"The entity with id={id} already exist")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_already_exist";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified id already exist";
}