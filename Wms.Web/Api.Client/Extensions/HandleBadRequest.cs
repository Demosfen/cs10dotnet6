using System.Net;
using System.Text.Json;
using Wms.Web.Api.Contracts.Extensions;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Api.Client.Extensions;

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
    
    public static async Task HandleUnprocessableEntityAsync(this HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            var content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<WmsProblemDetails>(content);
            problemDetails!.Instance = response.RequestMessage?.RequestUri?.ToString();
            throw new UnprocessableEntityException("API request failed!", problemDetails);
        }
    }
}
