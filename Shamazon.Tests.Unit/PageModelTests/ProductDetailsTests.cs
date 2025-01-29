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

public class ProductDetailsTests
{
    private readonly ProductDetails _sut;
    private readonly ILogger<ProductDetails> _logger = Substitute.For<ILogger<ProductDetails>>();
    private readonly IProductRepository _repo = Substitute.For<IProductRepository>();
    
    public ProductDetailsTests()
    {
        _sut = new ProductDetails(_logger, _repo);
        
        // Set up a mock request with a search string
        var context = new DefaultHttpContext { Request = { QueryString = new QueryString("?id=0") } };
        _sut.PageContext = new PageContext { HttpContext = context };
    }
    
    [Fact]
    public async Task OnGetAsync_LogsInfoAndSetsProductToNull_WhenIdIsNull()
    {
        await _sut.OnGetAsync(null);
        
        _logger.Received().Log(LogLevel.Information, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
        
        Assert.Null(_sut.Product);
    }
    
    [Fact]
    public async Task OnGetAsync_IdSetAndProductUpdated_WhenValidIdProvided()
    {
        const int productId = 45;
        var expectedProduct = new Product();
        _repo.GetProductByIdAsync(productId).Returns(expectedProduct);
        
        await _sut.OnGetAsync(productId);
        
        await _repo.Received(1).GetProductByIdAsync(productId);
        _sut.Id.Should().Be(productId);
        _sut.Product?.GetHashCode().Should().Be(expectedProduct.GetHashCode());
    }
    
    [Fact]
    public async Task OnGetAsync_LogsErrorAndRedirectsToErrorPage_WhenRepoThrowsException()
    {
        const int productId = 45;
        _repo.GetProductByIdAsync(productId).Throws(new Exception("Test exception"));
        
        var result = await _sut.OnGetAsync(productId);
        
        _logger.Received().Log(LogLevel.Error, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
        result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be("/Error");
    }
    
    [Fact]
    public async Task OnGetAsync_ProductSetToNull_WhenRepoReturnsNullProduct()
    {
        const int productId = 45;
        _repo.GetProductByIdAsync(productId).Returns((Product?)null);
        
        await _sut.OnGetAsync(productId);

        _sut.Id.Should().Be(45);
        _sut.Product.Should().BeNull();
    }
}