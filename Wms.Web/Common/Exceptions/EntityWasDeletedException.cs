namespace Wms.Web.Common.Exceptions;

public class EntityWasDeletedException : DomainException
{
    public Guid Id { get; }

    public EntityWasDeletedException() : base("Entity was deleted.")
    {
        
    }

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