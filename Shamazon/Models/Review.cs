using System.Text.Json.Serialization;

namespace Shamazon.Models;

/// <summary>
/// Model representing a review of a product in the Shamazon system.
/// </summary>
public class Review
{
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime Date { get; set; }
    
    public string? ReviewerName { get; set; }
    
    public string? ReviewerEmail { get; set; }
}