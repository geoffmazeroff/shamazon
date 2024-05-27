using System.Text.Json;
using Shamazon.Dto;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

public class ExternalProductRepository : IProductRepository
{
    // Temporary solution to cache products given the database of products
    // doesn't live on the same server as the application.
    protected Dictionary<int, Product> ProductCache { get; } = new();

    private const string ProductApiUrl = "https://dummyjson.com/products";
    private readonly ILogger<ExternalProductRepository> _logger;
    
    public ExternalProductRepository(ILogger<ExternalProductRepository> logger)
    {
        _logger = logger;
    }
    
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync()
    {
        // The 3rd-party API doesn't support getting just product headers, so we need to load all products.
        await LoadProductsAsync();
        
        return ProductCache.Values.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Title = p.Title,
            Category = p.Category,
            Price = p.Price,
            Rating = p.Rating,
            ThumbnailUrl = p.ThumbnailUrl
        }).ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await Task.FromResult(ProductCache[id]);
    }
    
    private async Task LoadProductsAsync()
    {
        var client = new HttpClient();
        var response = await client.GetAsync(ProductApiUrl);
        var json = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<ProductListWrapper>(json);
        if (products is null)
        {
            _logger.LogError("LoadProductsAsync(): Null deserialization from {Source}", ProductApiUrl);
            throw new InvalidOperationException("Failed to load products from external source.");
        }

        // No products leaves the dictionary in a clean state. The UI will handle.
        if (products.Products is null)
        {
            return;
        }
        
        foreach (var product in products.Products!)
        {
            ProductCache[product.Id] = product.FromProductDto();
        }
    }
}