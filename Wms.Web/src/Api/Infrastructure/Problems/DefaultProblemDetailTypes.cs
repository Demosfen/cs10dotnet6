using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Infrastructure.Problems;

/// <summary>
/// Default values for <see cref="ProblemDetails.Type"/>.
/// </summary>
internal static class DefaultProblemDetailTypes
{
    /// <summary>
    /// Value for <see cref="ProblemDetails.Type"/> if business logic related error has occured.
    /// </summary>
    /// <remarks>
    /// Used if only <see cref="IErrorDescription.ErrorCode"/> is null or empty.
    /// </remarks>
    public const string BusinessLogicException = "https://datatracker.ietf.org/doc/html/rfc7807";
}