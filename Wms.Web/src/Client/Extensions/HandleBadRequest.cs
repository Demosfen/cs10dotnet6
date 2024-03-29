using System.Net;
using System.Text.Json;
using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Extensions;

namespace Wms.Web.Client.Extensions;

internal static class HandleCommonExceptions
{
    public static async Task HandleBadRequestAsync(this HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<WmsProblemDetails>(content);
            problemDetails!.Instance = response.RequestMessage?.RequestUri?.ToString();
            problemDetails.Detail = "See the errors property for details.";
            throw new ApiValidationException("API request failed!", problemDetails);
        }
    }
}
