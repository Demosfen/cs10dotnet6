namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Initialization property error
/// </summary>
public class UninitializedPropertyException : DomainException
{
    /// <summary>
    /// Type name
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Initialization property exception
    /// </summary>
    /// <param name="typeName"></param>
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