using System.Text.Json;
using Shamazon.Dto;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

/// <summary>
/// A service for loading products from an external API.
/// </summary>
/// <remarks>Methods throw exceptions that are handled elsewhere.</remarks>
public class ExternalProductRepository : IProductRepository
{
    // Temporary solution to cache products given the database of products
    // doesn't live on the same server as the application.
    protected Dictionary<int, Product> ProductCache { get; } = new();
    protected DateTime CacheExpiration { get; set; } = DateTime.MinValue;
    private const int CacheDurationSeconds = 30;
    private const string ProductApiUrl = "https://dummyjson.com/products";
    
    private readonly ILogger<ExternalProductRepository> _logger;
    
    public ExternalProductRepository(ILogger<ExternalProductRepository> logger)
    {
        _logger = logger;
    }
    
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync(string search = "")
    {
        // The 3rd-party API doesn't support getting just product headers, so we need to load all products.
        await LoadProductsAsync();

        var products = ProductCache.Values.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Title = p.Title,
            Category = p.Category,
            Price = p.Price,
            Rating = p.Rating,
            ThumbnailUrl = p.ThumbnailUrl
        });
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Title!.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return products.OrderByDescending(p => p.Rating).ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await LoadProductsAsync();
        
        if (!ProductCache.TryGetValue(id, out var product))
        {
            _logger.LogWarning("ExternalProductRepository.GetProductByIdAsync() - Product with ID {ProductId} not found in the dictionary", id);
            return null;
        }
        return await Task.FromResult(product);
    }
    
    private async Task LoadProductsAsync()
    {
        var timeUntilCacheLoad = CacheExpiration - DateTime.UtcNow;
        _logger.LogInformation("Time until cache gets refreshed: {TimeUntilCacheReload} s; cache expires on {CacheExpireDateTime}", timeUntilCacheLoad.TotalSeconds, CacheExpiration);
        if (timeUntilCacheLoad.TotalSeconds >= 0)
        {
            return;
        }
        
        _logger.LogInformation("Time since last cache refresh ({TimeSinceLastLoad} s) triggered reload", timeUntilCacheLoad.TotalSeconds);

        var products = await LoadAndDeserializeJsonAsync(ProductApiUrl);
        
        if (products is null)
        {
            _logger.LogError("LoadProductsAsync(): Null deserialization from {Source}", ProductApiUrl);
            throw new InvalidOperationException("Failed to load products from external source.");
        }

        // Having no products leaves the dictionary in a clean state. The UI will handle.
        if (products.Products is null)
        {
            ProductCache.Clear();
            return;
        }
        
        foreach (var product in products.Products!)
        {
            ProductCache[product.Id] = product.FromProductDto();
        }

        CacheExpiration = DateTime.UtcNow + TimeSpan.FromSeconds(CacheDurationSeconds);
    }
    
    protected virtual async Task<ProductListJsonWrapper?> LoadAndDeserializeJsonAsync(string url)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var json = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<ProductListJsonWrapper>(json);
    }
}