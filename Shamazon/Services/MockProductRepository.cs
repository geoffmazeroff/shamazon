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
            new() { Id = 1, Title = "Product 1", Category = "Category 1", Price = 1.00m, Rating = 1.0m, ThumbnailData = imageBytes},
            new() { Id = 2, Title = "Product 2", Category = "Category 2", Price = 2.00m, Rating = 2.0m, ThumbnailData = imageBytes},
        };
    }
    
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync()
    {
        return await Task.FromResult(_demoProductViewModels);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}