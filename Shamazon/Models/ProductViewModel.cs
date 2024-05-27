namespace Shamazon.Models;

/// <summary>
/// Model representing a minimal set of product data in the Shamazon system.
/// </summary>
public class ProductViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public byte[]? ThumbnailData { get; set; }
}