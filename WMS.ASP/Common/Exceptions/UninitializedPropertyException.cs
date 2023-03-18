namespace WMS.ASP.Common.Exceptions;

public class UninitializedPropertyException : DomainException
{
    public string typeName { get; }

    public UninitializedPropertyException(string TypeName)
        : base($"The "+TypeName+" was not initialized")
    {
        typeName = TypeName;
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "unit_uninitialized";

    /// <inheritdoc />
    public override string ShortDescription => "Uninitialized unit";
}