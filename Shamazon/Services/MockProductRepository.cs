using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

public class MockProductRepository : IProductRepository
{
    private readonly List<ProductViewModel> _demoProductViewModels;
    
    public MockProductRepository(IWebHostEnvironment env)
    {
        var imagePath = Path.Combine(env.WebRootPath, "images", "defaultProduct.png");
        var imageBytes = File.ReadAllBytes(imagePath);
        
        _demoProductViewModels = new List<ProductViewModel>
        {
            new() { Title = "Product 1", Category = "Category 1", Price = 1.00m, Rating = 1.0m, ThumbnailData = imageBytes},
            new() { Title = "Product 2", Category = "Category 2", Price = 2.00m, Rating = 2.0m, ThumbnailData = imageBytes},
        };
    }
    
    public List<ProductViewModel> GetProductViewModels()
    {
        return _demoProductViewModels;
    }

    public Product GetProductById(int id)
    {
        throw new NotImplementedException();
    }
}