using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Extensions;

namespace Wms.Web.Common.Exceptions;

public class ApiValidationException : DomainException
{
    public WmsProblemDetails? ProblemDetails { get; }

    public ApiValidationException(string message, WmsProblemDetails? problemDetails) : base(message)
    {
        ProblemDetails = problemDetails;
    }

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