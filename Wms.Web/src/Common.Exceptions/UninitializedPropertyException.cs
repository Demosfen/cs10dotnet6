namespace Wms.Web.Common.Exceptions;

public class UninitializedPropertyException : DomainException
{
    public string TypeName { get; }

    public UninitializedPropertyException(string typeName)
        : base($"The "+typeName+" was not initialized")
    {
        TypeName = typeName;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "unit_uninitialized";

    /// <inheritdoc />
    public override string ShortDescription => "Uninitialized unit";
}