using Microsoft.Extensions.Logging;
using NSubstitute;
using Shamazon.Dto;
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
    
    // Tests to write for GetProductViewModelsAsync:
    // - Returns a list of items when no search criteria provided
    // - Sorts the list of items by rating when search string is provided or not
    // - Filters the list of items by search string when one is provided
    // - Does not filter the list of items when search string is not provided
    
    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }
    
    // Tests to write for GetProductByIdAsync:
    // - Returns a product when the product is found
    // - Returns null and logs a warning when the product is not found
    
    // Test to write for LoadProductsAsync:
    // - Does not update the cache when the cache is not expired
    // - Throws an exception when load and serialize operation throws an exception
    // - Logs an error and throws an exception when loaded list of products is null
    // - Resets the cache when there are no products (currently this will fail)
    // - Loads products into the cache and resets the cache expiration when there are products
}

public class ExternalProductRepoTestHelper : ExternalProductRepository
{
    private readonly ProductListJsonWrapper _listWrapperToBeMockLoaded;
    
    public ILogger<ExternalProductRepoTestHelper> Logger { get; }
    public ProductListJsonWrapper? ProductJsonToReturn { get; set; }
    
    public ExternalProductRepoTestHelper(ILogger<ExternalProductRepoTestHelper> logger) : base(logger)
    {
        Logger = logger;
    }

    protected override async Task<ProductListJsonWrapper?> LoadAndDeserializeJsonAsync(string url)
    {
        return await Task.FromResult(ProductJsonToReturn);
    }
}