using System.Text.Json;
using Wms.Web.Contracts.Extensions;

namespace Wms.Web.Common.Exceptions;

/// <summary>
/// Request validation error
/// </summary>
public class ApiValidationException : DomainException
{
    /// <summary>
    /// Problem details
    /// </summary>
    public WmsProblemDetails? ProblemDetails { get; }

    /// <summary>
    /// API request failure exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="problemDetails"></param>
    public ApiValidationException(string message, WmsProblemDetails? problemDetails) : base(message)
    {
        ProblemDetails = problemDetails;
    }

    /// <summary>
    /// API request failure exception
    /// </summary>
    /// <param name="response"></param>
    public ApiValidationException(HttpResponseMessage response) : base("API request failed")
    {
        var responseContent = response.Content.ReadAsStringAsync().Result;
        ProblemDetails = JsonSerializer.Deserialize<WmsProblemDetails>(responseContent);
    }
    
    /// <inheritdoc />
    public override string ErrorCode => "incorrect_http_request";

    /// <inheritdoc />
    public override string ShortDescription => "One of the request property is incorrect";
}