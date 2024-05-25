namespace Shamazon.Models;

/// <summary>
/// Model representing metadata for a product in the Shamazon system.
/// </summary>
public class ProductMetadata
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Barcode { get; set; }
    public string? QrCodeUrl { get; set; }
}