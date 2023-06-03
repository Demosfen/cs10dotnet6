using System.Text.Json.Serialization;

namespace Wms.Web.Contracts.Extensions;

public class WmsProblemDetails
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("status")]
    public int Status { get; set; }
    
    [JsonPropertyName("detail")]
    public string? Detail { get; set; }
    
    [JsonPropertyName("instance")]
    public string? Instance { get; set; }
    
    [JsonPropertyName("errors")]
    public IDictionary<string, string[]>? Errors { get; set; }
}