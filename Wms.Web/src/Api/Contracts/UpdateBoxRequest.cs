using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

/// <summary>
/// Contract for updating a box
/// </summary>
public sealed class UpdateBoxRequest
{
    /// <summary>
    /// Box ID
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Palette ID where box is located
    /// </summary>
    public required Guid PaletteId { get; init; }
    
    public required BoxRequest BoxRequest { get; init; }
}