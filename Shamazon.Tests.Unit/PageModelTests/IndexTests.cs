using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shamazon.Interfaces;
using Shamazon.Models;
using Shamazon.Pages;

namespace Shamazon.Tests.PageModelTests;

public class IndexTests
{
    private readonly IndexModel _sut;
    private readonly ILogger<IndexModel> _logger = Substitute.For<ILogger<IndexModel>>();
    private readonly IProductRepository _repo = Substitute.For<IProductRepository>();
    private readonly IImageLoader _imageLoader = Substitute.For<IImageLoader>();
    private const string SearchString = "Bob";
    
    public IndexTests()
    {
        _sut = new IndexModel(_logger, _repo, _imageLoader);
        
        // Set up a mock request with a search string
        var context = new DefaultHttpContext { Request = { QueryString = new QueryString($"?search={SearchString}") } };
        _sut.PageContext = new PageContext { HttpContext = context };
    }
    
    [Fact]
    public async void OnGetAsync_CallsRepoWithSearchString()
    {
        _repo.GetProductViewModelsAsync(SearchString).Returns([]);
        
        await _sut.OnGetAsync();
        
        await _repo.Received(1).GetProductViewModelsAsync(SearchString);
    }
    
    [Fact]
    public async void OnGetAsync_SetsProductViewModels_WhenRepoReturnsData()
    {
        var products = new List<ProductViewModel> { new(), new() };
        _repo.GetProductViewModelsAsync(Arg.Any<string>()).Returns(products);
        
        await _sut.OnGetAsync();
        
        _sut.ProductViewModels.Should().HaveCount(2);
    }
    
    [Fact]
    public async void OnGetAsync_UsesImageLoaderToSetThumbnailData_WhenRepoReturnsData()
    {
        var products = new List<ProductViewModel>
        {
            new() { ThumbnailUrl = "abc"}, 
            new() { ThumbnailUrl = "xyz" }
        };
        _repo.GetProductViewModelsAsync(Arg.Any<string>()).Returns(products);
        
        await _sut.OnGetAsync();
        
        await _imageLoader.Received(1).LoadImageAsync("abc");
        await _imageLoader.Received(1).LoadImageAsync("xyz");
    }
    
    [Fact]
    public async void OnGetAsync_DoesNotUseImageLoaderToSetThumbnailData_IfThumbnailDataAlreadyPresent()
    {
        var products = new List<ProductViewModel>
        {
            new() { ThumbnailData = [1, 2] }, 
            new() { ThumbnailData = [3, 4] }
        };
        _repo.GetProductViewModelsAsync(Arg.Any<string>()).Returns(products);
        
        await _sut.OnGetAsync();
        
        await _imageLoader.Received(0).LoadImageAsync(Arg.Any<string>());
    }
    
    [Fact]
    public async void OnGetAsync_LogsErrorAndSetsThumbnailDataToEmptyArray_IfImageLoaderFails()
    {
        var products = new List<ProductViewModel>
        {
            new()
            {
                ThumbnailUrl = "xyz",
                ThumbnailData = null
            }, 
        };
        _repo.GetProductViewModelsAsync(Arg.Any<string>()).Returns(products);
        _imageLoader.LoadImageAsync(Arg.Any<string>()).Throws(new Exception("Test"));
        
        await _sut.OnGetAsync();
        
        _logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
        _sut.ProductViewModels[0].ThumbnailData.Should().BeEmpty();
    }
    
    [Fact]
    public async void OnGetAsync_LogsErrorAndRedirectsToErrorPage_IfRepoFails()
    {
        _repo.GetProductViewModelsAsync(Arg.Any<string>()).Throws(new Exception("Test"));
        
        var result = await _sut.OnGetAsync();

        result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("/Error");
    }
    
    // OnGetAsync tests:
    // - Logs errors and redirects to error page when the product repo fails
}