using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class UpdateBoxRequest
{
    [FromRoute(Name = "boxId")]
    public required Guid Id { get; init; }
    
    [FromQuery]
    public required Guid PaletteId { get; init; }
    
    [FromBody]
    public required BoxRequest BoxRequest { get; init; }
}