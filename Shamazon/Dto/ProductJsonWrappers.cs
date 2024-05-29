using System.Text.Json.Serialization;

namespace Shamazon.Dto;

/// <summary>
/// DTO for obtaining product data from JSON.
/// </summary>
/// <remarks>
/// This wrapper gets mapped to a data model that isn't dependent
/// on how the data is ingested.
/// </remarks>
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
    public DimensionJsonWrapper? Dimensions { get; set; }
    
    [JsonPropertyName("warrantyInformation")]
    public string? WarrantyInformation { get; set; }
    
    [JsonPropertyName("shippingInformation")]
    public string? ShippingInformation { get; set; }
    
    [JsonPropertyName("availabilityStatus")]
    public string? AvailabilityStatus { get; set; }
    
    [JsonPropertyName("reviews")]
    public List<ReviewJsonWrapper>? Reviews { get; set; }
    
    [JsonPropertyName("returnPolicy")]
    public string? ReturnPolicy { get; set; }
    
    [JsonPropertyName("minimumOrderQuantity")]
    public int MinimumOrderQuantity { get; set; }
    
    [JsonPropertyName("meta")]
    public ProductMetadataWrapper? Metadata { get; set; }
    
    [JsonPropertyName("images")]
    public List<string> ImageUrls { get; set; } = [];
    
    [JsonPropertyName("thumbnail")]
    public string? ThumbnailUrl { get; set; }
}

/// <summary>
/// Object model (DTO) for obtaining product list data from JSON.
/// </summary>
/// <remarks>
/// This wrapper gets mapped to a data model that isn't dependent
/// on how the data is ingested.
/// </remarks>
public class ProductListJsonWrapper
{
    [JsonPropertyName("products")]
    public List<ProductJsonWrapper>? Products { get; set; }
}

/// <summary>
/// Object model (DTO) for obtaining product review data from JSON.
/// </summary>
/// <remarks>
/// This wrapper gets mapped to a data model that isn't dependent
/// on how the data is ingested.
/// </remarks>
public class ReviewJsonWrapper
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

/// <summary>
/// DTO for obtaining product metadata from JSON.
/// </summary>
/// <remarks>
/// This wrapper gets mapped to a data model that isn't dependent
/// on how the data is ingested.
/// </remarks>
public class ProductMetadataWrapper
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

/// <summary>
/// DTO for obtaining product dimensions from JSON.
/// </summary>
/// <remarks>
/// This wrapper gets mapped to a data model that isn't dependent
/// on how the data is ingested.
/// </remarks>
public class DimensionJsonWrapper
{
    [JsonPropertyName("height")]
    public decimal Height { get; set; }
    
    [JsonPropertyName("width")]
    public decimal Width { get; set; }
    
    [JsonPropertyName("depth")]
    public decimal Depth { get; set; }
}