namespace Wms.Web.Common.Exceptions;

public class DomainException : Exception
{
    /// <summary>
    /// Error code
    /// </summary>
    public virtual string ErrorCode => string.Empty;

    /// <summary>
    /// Error short description
    /// </summary>
    public virtual string ShortDescription => string.Empty;

    /// <inheritdoc />
    protected DomainException()
    {
    }

    /// <inheritdoc />
    protected DomainException(string message) : base(message)
    {
    }

    /// <inheritdoc />
    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}