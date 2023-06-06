using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

/// <summary>
/// Contract for creating a box
/// </summary>
public sealed class CreateBoxRequest
{
    /// <summary>
    /// Palette ID where box is located
    /// </summary>
    public required Guid PaletteId { get; init; }
    
    /// <summary>
    /// Box ID
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Contract (body) for creating a box
    /// </summary>
    public required BoxRequest BoxRequest { get; init; }
}