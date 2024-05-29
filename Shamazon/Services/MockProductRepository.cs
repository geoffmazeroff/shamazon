using System.Text.Json;
using Shamazon.Dto;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

/// <summary>
/// A mock implementation of the IProductRepository interface for testing purposes.
/// </summary>
public class MockProductRepository : IProductRepository
{
    private List<ProductViewModel> _demoProductViewModels = new();
    private Product? _demoProduct;
    
    public MockProductRepository(IWebHostEnvironment env)
    {
        var imagePath = Path.Combine(env.WebRootPath, "images", "defaultProduct.png");
        var imageBytes = File.ReadAllBytes(imagePath);
        
        CreateProductViewModels(imageBytes);
        //CreateProductManually();
        CreateProductFromJson();
    }
  
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync(string search = "")
    {
        var sortedProducts = _demoProductViewModels.OrderByDescending(p => p.Rating).ToList();
        return await Task.FromResult(sortedProducts);
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
    
    private void CreateProductManually()
    {
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
            Dimensions = new Dimension { Width = 10, Height = 5, Depth = 3 },
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

    private void CreateProductViewModels(byte[] imageBytes)
    {
        _demoProductViewModels =
        [
          new ProductViewModel
          {
            Id = 1, Title = "Product 1", Category = "Category 1", Price = 1.00m, Rating = 1.0m,
            ThumbnailData = imageBytes
          },
          new ProductViewModel
          {
            Id = 2, Title = "Product 2", Category = "Category 2", Price = 2.00m, Rating = 2.0m,
            ThumbnailData = imageBytes
          },
          new ProductViewModel
          {
            Id = 10, Title = "Bad Product", Category = "Error Testing", Price = 10.00m, Rating = 0.0m,
            ThumbnailData = imageBytes
          }
        ];
    }

    private void CreateProductFromJson()
    {
        var jsonString = GetTwoProductsAsJson();
        var products = JsonSerializer.Deserialize<ProductListJsonWrapper>(jsonString);

        if (products?.Products is null)
        {
          return;
        }
        _demoProduct = products.Products.First().FromProductDto();
    }
    
    private static string GetTwoProductsAsJson()
    {
        return """
             {
               "products": [
                 {
                   "id": 1,
                   "title": "Essence Mascara Lash Princess",
                   "description": "The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effects. Achieve dramatic lashes with this long-lasting and cruelty-free formula.",
                   "category": "beauty",
                   "price": 9.99,
                   "discountPercentage": 7.17,
                   "rating": 4.94,
                   "stock": 5,
                   "tags": [
                     "beauty",
                     "mascara"
                   ],
                   "brand": "Essence",
                   "sku": "RCH45Q1A",
                   "weight": 2,
                   "dimensions": {
                     "width": 23.17,
                     "height": 14.43,
                     "depth": 28.01
                   },
                   "warrantyInformation": "1 month warranty",
                   "shippingInformation": "Ships in 1 month",
                   "availabilityStatus": "Low Stock",
                   "reviews": [
                     {
                       "rating": 2,
                       "comment": "Very unhappy with my purchase!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "John Doe",
                       "reviewerEmail": "john.doe@x.dummyjson.com"
                     },
                     {
                       "rating": 2,
                       "comment": "Not as described!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "Nolan Gonzalez",
                       "reviewerEmail": "nolan.gonzalez@x.dummyjson.com"
                     },
                     {
                       "rating": 5,
                       "comment": "Very satisfied!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "Scarlett Wright",
                       "reviewerEmail": "scarlett.wright@x.dummyjson.com"
                     }
                   ],
                   "returnPolicy": "30 days return policy",
                   "minimumOrderQuantity": 24,
                   "meta": {
                     "createdAt": "2024-05-23T08:56:21.618Z",
                     "updatedAt": "2024-05-23T08:56:21.618Z",
                     "barcode": "9164035109868",
                     "qrCode": "https://dummyjson.com/public/qr-code.png"
                   },
                   "images": [
                     "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/1.png"
                   ],
                   "thumbnail": "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/thumbnail.png"
                 },
                 {
                   "id": 2,
                   "title": "Eyeshadow Palette with Mirror",
                   "description": "The Eyeshadow Palette with Mirror offers a versatile range of eyeshadow shades for creating stunning eye looks. With a built-in mirror, it's convenient for on-the-go makeup application.",
                   "category": "beauty",
                   "price": 19.99,
                   "discountPercentage": 5.5,
                   "rating": 3.28,
                   "stock": 44,
                   "tags": [
                     "beauty",
                     "eyeshadow"
                   ],
                   "brand": "Glamour Beauty",
                   "sku": "MVCFH27F",
                   "weight": 3,
                   "dimensions": {
                     "width": 12.42,
                     "height": 8.63,
                     "depth": 29.13
                   },
                   "warrantyInformation": "1 year warranty",
                   "shippingInformation": "Ships in 2 weeks",
                   "availabilityStatus": "In Stock",
                   "reviews": [
                     {
                       "rating": 4,
                       "comment": "Very satisfied!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "Liam Garcia",
                       "reviewerEmail": "liam.garcia@x.dummyjson.com"
                     },
                     {
                       "rating": 1,
                       "comment": "Very disappointed!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "Nora Russell",
                       "reviewerEmail": "nora.russell@x.dummyjson.com"
                     },
                     {
                       "rating": 5,
                       "comment": "Highly impressed!",
                       "date": "2024-05-23T08:56:21.618Z",
                       "reviewerName": "Elena Baker",
                       "reviewerEmail": "elena.baker@x.dummyjson.com"
                     }
                   ],
                   "returnPolicy": "30 days return policy",
                   "minimumOrderQuantity": 32,
                   "meta": {
                     "createdAt": "2024-05-23T08:56:21.618Z",
                     "updatedAt": "2024-05-23T08:56:21.618Z",
                     "barcode": "2817839095220",
                     "qrCode": "https://dummyjson.com/public/qr-code.png"
                   },
                   "images": [
                     "https://cdn.dummyjson.com/products/images/beauty/Eyeshadow%20Palette%20with%20Mirror/1.png"
                   ],
                   "thumbnail": "https://cdn.dummyjson.com/products/images/beauty/Eyeshadow%20Palette%20with%20Mirror/thumbnail.png"
                 }
               ]
             }
             """;
    }
}