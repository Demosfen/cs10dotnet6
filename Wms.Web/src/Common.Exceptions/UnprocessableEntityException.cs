using System.Text.Json;
using Wms.Web.Contracts.Extensions;

namespace Wms.Web.Common.Exceptions;

public class UnprocessableEntityException : DomainException
{
    public WmsProblemDetails? ProblemDetails { get; }

    public UnprocessableEntityException(string message, WmsProblemDetails? problemDetails) : base(message)
    {
        ProblemDetails = problemDetails;
    }

    public UnprocessableEntityException(HttpResponseMessage response) : base("API request failed")
    {
        var responseContent = response.Content.ReadAsStringAsync().Result;
        ProblemDetails = JsonSerializer.Deserialize<WmsProblemDetails>(responseContent);
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "unprocessable_entity";

    /// <inheritdoc />
    public override string ShortDescription => "Unprocessable entity properties was requested";
}