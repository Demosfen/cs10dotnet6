namespace Wms.Web.Common.Exceptions;

public class EntityExpiryDateException : DomainException
{
    public Guid Id { get; }

    public EntityExpiryDateException() : base("Entity expiry date incorrect!")
    {
        
    }

    public EntityExpiryDateException(Guid id)
        : base($"The entity with id={id} has incorrect Expiry date. Probably Expiry lower than Production date.")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_expiry_incorrect";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified Expiry date cannot be created";
}