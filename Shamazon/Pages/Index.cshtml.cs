using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IImageLoader _imageLoader;

    public List<ProductViewModel> ProductViewModels { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository, IImageLoader imageLoader)
    {
        _logger = logger;
        _productRepository = productRepository;
        _imageLoader = imageLoader;
        
        ProductViewModels = new List<ProductViewModel>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var searchString = Request.Query["search"].ToString();
        _logger.LogInformation("Index.OnGetAsync() - Found the following search string query: {QueryString}", searchString);

        try
        {
            ProductViewModels = await _productRepository.GetProductViewModelsAsync(searchString);    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Index.OnGetAsync() - Failed to retrieve product view models from the repository");
            return RedirectToPage("/Error");
        }
        
        // The thumbnails are URLs, so we need to load the image content.
        // Note: The image loader service will return a default image if the image fails to load.
        // Should that process fail, we will log the error and set the thumbnail data to an empty byte array.
        foreach (var product in ProductViewModels)
        {
            // Skip if we've already got the thumbnail data.
            if (product.ThumbnailData is not null)
            {
                continue;
            }
            
            try
            {
                product.ThumbnailData = await _imageLoader.LoadImageAsync(product.ThumbnailUrl);    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Index.OnGetAsync() - Failed to load any image for product {ProductId}", product.Id);
                product.ThumbnailData = [];
            }
        }

        return Page();
    }
}