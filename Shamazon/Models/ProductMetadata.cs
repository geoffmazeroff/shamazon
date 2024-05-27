using System.Text.Json.Serialization;

namespace Shamazon.Models;

/// <summary>
/// Model representing metadata for a product in the Shamazon system.
/// </summary>
public class ProductMetadata
{
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    [JsonPropertyName("barcode")]
    public string? Barcode { get; set; }
    
    [JsonPropertyName("qrCode")]
    public string? QrCodeUrl { get; set; }
}