using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class UpdateBoxRequest
{
    [FromRoute(Name = "boxId")]
    public required Guid Id { get; init; }
    
    [FromQuery]
    public required Guid PaletteId { get; init; }
    
    [FromBody]
    public required BoxRequest BoxRequest { get; init; }
}