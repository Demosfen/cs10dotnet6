namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Entity oversize error
/// </summary>
public class UnitOversizeException : DomainException
{
    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Entity oversize exception
    /// </summary>
    /// <param name="id"></param>
    public UnitOversizeException(Guid id)
        : base($"The unit with id={id} does not match the dimensions of the pallet")
    {
        Id = id;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "unit_oversize";

    /// <inheritdoc />
    public override string ShortDescription => "The box does not match the dimensions of the pallet";
}