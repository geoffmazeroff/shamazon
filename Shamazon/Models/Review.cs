using System.Text.Json.Serialization;

namespace Shamazon.Models;

/// <summary>
/// Model representing a review of a product in the Shamazon system.
/// </summary>
public class Review
{
    [JsonPropertyName("rating")]
    public int Rating { get; set; }
    
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
    
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("reviewerName")]
    public string? ReviewerName { get; set; }
    
    [JsonPropertyName("reviewerEmail")]
    public string? ReviewerEmail { get; set; }
}