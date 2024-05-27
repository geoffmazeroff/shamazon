using System.Text.Json.Serialization;

namespace Shamazon.Models;

public class ProductJsonWrapper
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("category")]
    public string? Category { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("discountPercentage")]
    public decimal DiscountPercentage { get; set; }
    
    [JsonPropertyName("rating")]
    public decimal Rating { get; set; }
    
    [JsonPropertyName("stock")]
    public int Stock { get; set; }
    
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = [];
    
    [JsonPropertyName("brand")]
    public string? Brand { get; set; }
    
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }
    
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    
    [JsonPropertyName("dimensions")]
    public Dictionary<string, Dimension> Dimensions { get; set; }
    
    [JsonPropertyName("warrantyInformation")]
    public string? WarrantyInformation { get; set; }
    
    [JsonPropertyName("shippingInformation")]
    public string? ShippingInformation { get; set; }
    
    [JsonPropertyName("availabilityStatus")]
    public string? AvailabilityStatus { get; set; }
    
    [JsonPropertyName("reviews")]
    public ReviewListWrapper Reviews { get; set; }
    
    [JsonPropertyName("returnPolicy")]
    public string? ReturnPolicy { get; set; }
    
    [JsonPropertyName("minimumOrderQuantity")]
    public int MinimumOrderQuantity { get; set; }
    
    [JsonPropertyName("meta")]
    public Dictionary<string, ProductMetadata> Metadata { get; set; }
    
    [JsonPropertyName("images")]
    public List<string> ImageUrls { get; set; } = [];
    
    [JsonPropertyName("thumbnail")]
    public string? ThumbnailUrl { get; set; }
}

public class ReviewListWrapper
{
    [JsonPropertyName("reviews")]  
    public List<Review>? Reviews { get; set; }
}

public class ProductListWrapper
{
    [JsonPropertyName("products")]
    public List<Product>? Products { get; set; }
} 