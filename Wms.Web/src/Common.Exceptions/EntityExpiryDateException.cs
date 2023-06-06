namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity expiry date error
/// </summary>
public class EntityExpiryDateException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Incorrect entity expiry date exception
    /// </summary>
    /// <param name="id"></param>
    public EntityExpiryDateException() : base("Entity expiry date incorrect!")
    {
        
    }

    /// <summary>
    /// Incorrect entity expiry date exception
    /// </summary>
    /// <param name="id"></param>
    public EntityExpiryDateException(Guid id)
        : base($"The entity with id={id} has incorrect Expiry date. " +
               $"Probably Expiry lower than Production date, or both dates is null.")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "entity_expiry_incorrect";

    /// <inheritdoc />
    public override string ShortDescription => "The entity with specified Expiry and Production dates cannot be created";
}