using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

/// <summary>
/// A mock implementation of the IProductRepository interface for testing purposes.
/// </summary>
public class MockProductRepository : IProductRepository
{
    private readonly List<ProductViewModel> _demoProductViewModels;
    private readonly Product? _demoProduct;
    
    public MockProductRepository(IWebHostEnvironment env)
    {
        var imagePath = Path.Combine(env.WebRootPath, "images", "defaultProduct.png");
        var imageBytes = File.ReadAllBytes(imagePath);
        
        _demoProductViewModels = new List<ProductViewModel>
        {
            new() { Id = 1, Title = "Product 1", Category = "Category 1", Price = 1.00m, Rating = 1.0m, ThumbnailData = imageBytes},
            new() { Id = 2, Title = "Product 2", Category = "Category 2", Price = 2.00m, Rating = 2.0m, ThumbnailData = imageBytes},
            new() { Id = 10, Title = "Bad Product", Category = "Error Testing", Price = 10.00m, Rating = 0.0m, ThumbnailData = imageBytes},
        };
        
        // Create a Product instance with placeholder values for each of the properties.
        _demoProduct = new Product
        {
            Id = 1,
            Title = "Product 1",
            Description = "This is a placeholder description for Product 1.",
            Category = "Category 1",
            Price = 1.00m,
            DiscountPercentage = 0.10m,
            Rating = 1.0m,
            Stock = 100,
            Tags = ["Tag1", "Tag2", "Tag3"],
            Brand = "Acme Inc.",
            Sku = "123ABC-0",
            Weight = 5,
            Dimensions = new Dimension { Depth = 0.5m, Width = 0.6m, Height = 2.0m },
            WarrantyInformation = "1 month limited warranty",
            ShippingInformation = "Shipping within the US only",
            AvailabilityStatus = "In Stock",
            Reviews =
            [
                new Review { Rating = 5, Comment = "Great product!", ReviewerName = "Bob", ReviewerEmail = "bob@shamazon.com" },
                new Review { Rating = 4, Comment = "Good value for the price.", ReviewerName = "Greta", ReviewerEmail = "greta@shamazon.com" }
            ],
            ReturnPolicy = "30 days return policy",
            MinimumOrderQuantity = 1,
            Metadata = new ProductMetadata
            {
                CreatedAt = DateTime.Now - TimeSpan.FromDays(3), 
                UpdatedAt = DateTime.Now,
                Barcode = "1100494949",
                QrCodeUrl = "/images/qr-code.png"
            },
            ImageUrls = [ "/images/product1.jpg", "/images/product1-2.jpg"],
            ThumbnailUrl = "/images/product1-thumb.jpg"
        };
    }
    
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync()
    {
        return await Task.FromResult(_demoProductViewModels);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        // Simulate a product not found by only returning the single demo product for IDs less than 5
        if (id < 5)
        {
            return await Task.FromResult(_demoProduct);
        }

        return null;
    }
}