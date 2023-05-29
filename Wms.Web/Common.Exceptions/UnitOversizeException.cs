namespace Wms.Web.Common.Exceptions;

public class UnitOversizeException : DomainException
{
    public Guid Id { get; }

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