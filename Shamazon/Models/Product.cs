namespace Shamazon.Models;

/// <summary>
/// Model representing a product in the Shamazon system.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Rating { get; set; }
    public int Stock { get; set; }
    public List<string> Tags { get; set; } = [];
    public string? Brand { get; set; }
    public string? Sku { get; set; }
    public int Weight { get; set; }
    public Dimension? Dimensions { get; set; }
    public string? WarrantyInformation { get; set; }
    public string? ShippingInformation { get; set; }
    public string? AvailabilityStatus { get; set; }
    public List<Review> Reviews { get; set; } = [];
    public string? ReturnPolicy { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public ProductMetadata? Metadata { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public string? ThumbnailUrl { get; set; }
}