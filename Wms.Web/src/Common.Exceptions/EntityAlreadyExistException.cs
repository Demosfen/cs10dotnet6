namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity conflict: entity already exist error
/// </summary>
public class EntityAlreadyExistException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Entity already exist exception
    /// </summary>
    /// <param name="id"></param>
    public EntityAlreadyExistException() : base("Entities were not found!")
    {
        
    }

    /// <summary>
    /// Entity already exist exception
    /// </summary>
    /// <param name="id"></param>
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