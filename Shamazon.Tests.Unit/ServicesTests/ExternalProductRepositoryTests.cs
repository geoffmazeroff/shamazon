using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shamazon.Dto;
using Shamazon.Models;
using Shamazon.Services;

namespace Shamazon.Tests.ServicesTests;

public class ExternalProductRepositoryTests
{
    private readonly ExternalProductRepoTestHelper _sut;
    private readonly ILogger<ExternalProductRepoTestHelper> _logger = Substitute.For<ILogger<ExternalProductRepoTestHelper>>();
    
    public ExternalProductRepositoryTests()
    {
        _sut = new ExternalProductRepoTestHelper(_logger);
    }
    
    [Fact]
    public async void GetProductViewModelsAsync_ReturnsAListOfItems_WhenNoSearchCriteriaProvided()
    {
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();

        var viewModels = await _sut.GetProductViewModelsAsync();
        
        viewModels.Should().HaveCount(3);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("Bob")]
    public async void GetProductViewModelsAsync_ReturnsAListOfItems_WhenSearchCriteriaIsEmpty(string search)
    {
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();

        var viewModels = await _sut.GetProductViewModelsAsync(search);
        
        viewModels.Should().BeInDescendingOrder(p => p.Rating);
    }
    
    [Fact]
    public async void GetProductViewModelsAsync_ReturnsAFilteredListOfItems_WhenSearchCriteriaProvided()
    {
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();

        var viewModels = await _sut.GetProductViewModelsAsync("Bob");
        
        viewModels.Should().HaveCount(2);
    }
    
    [Fact]
    public async void GetProductByIdAsync_ReturnsAProduct_WhenProductShouldExist()
    {
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();

        var product = await _sut.GetProductByIdAsync(1);
        
        product.Should().NotBeNull();
        product.Id.Should().Be(1);
    }
    
    [Fact]
    public async void GetProductByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();

        var product = await _sut.GetProductByIdAsync(4);
        
        product.Should().BeNull();
    }
    
    [Fact]
    public async void LoadProductsAsync_DoesNotUpdateTheCache_WhenCacheHasNotExpired()
    {
        // One cached item, and cache expires 15 minutes from now
        const int cachedProductId = 1;
        _sut.CacheExpirationSeam = DateTime.UtcNow.AddMinutes(15);
        _sut.ProductCacheSeam[cachedProductId] = new Product();
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();  // If cache were refreshed, there would be 3 items instead of 1

        await _sut.GetProductByIdAsync(cachedProductId);
        
        _sut.ProductCacheSeam.Should().HaveCount(1);
    }
    
    [Fact]
    public async void LoadProductsAsync_UpdatesTheCache_WhenCacheHasExpired()
    {
        // One cached item, and cache expired 15 minutes ago
        const int cachedProductId = 1;
        _sut.CacheExpirationSeam = DateTime.UtcNow.AddMinutes(-15);
        _sut.ProductCacheSeam[cachedProductId] = new Product();
        _sut.ProductJsonToReturn = CreateThreeDummyProducts();  // If cache were refreshed, there would be 3 items instead of 1

        await _sut.GetProductByIdAsync(cachedProductId);
        
        _sut.ProductCacheSeam.Should().HaveCount(3);
    }
    
    [Fact]
    public async void LoadProductsAsync_ResetsTheCache_WhenThereAreNoProductsToAssign()
    {
        // One cached item, and cache expired 15 minutes ago
        const int cachedProductId = 1;
        _sut.CacheExpirationSeam = DateTime.UtcNow.AddMinutes(-15);
        _sut.ProductCacheSeam[cachedProductId] = new Product();
        _sut.ProductJsonToReturn = new ProductListJsonWrapper
        {
            Products = null
        };

        await _sut.GetProductByIdAsync(cachedProductId);
        
        _sut.ProductCacheSeam.Should().HaveCount(0);
    }
    
    [Fact]
    public async void LoadProductsAsync_ThrowsAnException_WhenLoadAndSerializeOperationThrows()
    {
        // One cached item, and cache expired 15 minutes ago
        const int cachedProductId = 1;
        _sut.CacheExpirationSeam = DateTime.UtcNow.AddMinutes(-15);
        _sut.LoadShouldThrowException = true;

        var exceptionThrown = false;
        try
        {
            await _sut.GetProductByIdAsync(cachedProductId);    
        }
        catch (Exception)
        {
            exceptionThrown = true;
        }
        
        exceptionThrown.Should().BeTrue();
    }
    
    [Fact]
    public async void LoadProductsAsync_LogsErrorAndThrowsAnException_WhenLoadedListIsNull()
    {
        // One cached item, and cache expired 15 minutes ago
        const int cachedProductId = 1;
        _sut.CacheExpirationSeam = DateTime.UtcNow.AddMinutes(-15);
        _sut.ProductJsonToReturn = null;

        var exceptionThrown = false;
        try
        {
            await _sut.GetProductByIdAsync(cachedProductId);    
        }
        catch (InvalidOperationException) // Check that the correct exception was thrown
        {
            exceptionThrown = true;
        }
        
        exceptionThrown.Should().BeTrue();
        _logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }
   
    private static ProductListJsonWrapper CreateThreeDummyProducts()
    {
        return new ProductListJsonWrapper
        {
            Products = new List<ProductJsonWrapper>
            {
                new()
                {
                    Id = 1,
                    Title = "Bob",
                    Dimensions = new DimensionJsonWrapper(),
                    Metadata = new ProductMetadataWrapper(),
                    Reviews = new List<ReviewJsonWrapper>(),
                    Rating = 3m
                },
                new()
                {
                    Id = 2,
                    Title = "Bobby",
                    Dimensions = new DimensionJsonWrapper(),
                    Metadata = new ProductMetadataWrapper(),
                    Reviews = new List<ReviewJsonWrapper>(),
                    Rating = 4m
                },
                new()
                {
                    Id = 3,
                    Title = "John",
                    Dimensions = new DimensionJsonWrapper(),
                    Metadata = new ProductMetadataWrapper(),
                    Reviews = new List<ReviewJsonWrapper>(),
                    Rating = 5m
                }
            }
        };
    }
}

public class ExternalProductRepoTestHelper : ExternalProductRepository
{
    public ILogger<ExternalProductRepoTestHelper> Logger { get; }
    public ProductListJsonWrapper? ProductJsonToReturn { get; set; }

    public bool LoadShouldThrowException { get; set; }
    
    public ExternalProductRepoTestHelper(ILogger<ExternalProductRepoTestHelper> logger) : base(logger)
    {
        Logger = logger;
    }

    protected override async Task<ProductListJsonWrapper?> LoadAndDeserializeJsonAsync(string url)
    {
        if (!LoadShouldThrowException)
        {
            return await Task.FromResult(ProductJsonToReturn);
        }
        
        throw new Exception("Test exception");
    }
    
    // Not a fan of this approach. Ultimately the repository wouldn't *implement* a cache -- it would *use* one.
    // Regardless, I'm leaving this here to show how one would unit test this scenario.
    public DateTime CacheExpirationSeam
    {
        get => CacheExpiration;
        set => CacheExpiration = value;
    }

    public Dictionary<int, Product> ProductCacheSeam => ProductCache;
}