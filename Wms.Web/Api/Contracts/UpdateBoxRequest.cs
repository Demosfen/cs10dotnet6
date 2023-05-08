using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class UpdateBoxRequest
{
    public required Guid Id { get; init; }
    
    public required Guid PaletteId { get; init; }
    
    public required BoxRequest BoxRequest { get; init; }
}