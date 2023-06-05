namespace Wms.Web.Contracts.Requests;

/// <summary>
/// Box dto (contract) for create/update/delete operations
/// </summary>
public class BoxRequest
{
    /// <summary>
    /// Box width (should be lower than palette width)
    /// </summary>
    public required decimal Width { get; set; }
    
    /// <summary>
    /// Box height (should be lower than palette height)
    /// </summary>
    public required decimal Height { get; set; }

    /// <summary>
    /// Box depth (should be lower than palette depth)
    /// </summary>
    public required decimal Depth { get; set; }
    
    /// <summary>
    /// Box weight
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Box production date (should be lower than expiry date)
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    
    /// <summary>
    /// Box expiry date (should be greater than production date)
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}