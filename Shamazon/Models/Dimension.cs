using System.Text.Json.Serialization;

namespace Shamazon.Models;

/// <summary>
/// Model representing the dimensions of a product in the Shamazon system.
/// </summary>
public class Dimension
{
    [JsonPropertyName("height")]
    public decimal Height { get; set; }
    
    [JsonPropertyName("width")]
    public decimal Width { get; set; }
    
    [JsonPropertyName("depth")]
    public decimal Depth { get; set; }
}