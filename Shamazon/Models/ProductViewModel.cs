namespace Shamazon.Models;

/// <summary>
/// Model representing a minimal set of product data in the Shamazon system.
/// </summary>
/// <remarks>
/// An alternative was to have this be a base model that Product would inherit from.
/// This approach was chosen to keep the models separate and allow this model to
/// change without having the Product model change.
/// </remarks>
public class ProductViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    
    /// <summary>
    /// The bytes for the image. 
    /// </summary>
    /// <remarks>
    /// There's likely a better way to handle this object model (Liskov substitution principle).
    /// The URL string is retrieved first, then the image data is loaded. This byte array isn't
    /// populated until later.
    /// </remarks>
    public byte[]? ThumbnailData { get; set; }
    public string? ThumbnailUrl { get; set; }
}