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
        ProductViewModels = await _productRepository.GetProductViewModelsAsync();
        
        // The thumbnails are URLs, so we need to load the image content.
        // Note: The image loader service will return a default image if the image fails to load.
        // Should that process fail, we will log the error and set the thumbnail data to an empty byte array.
        foreach (var product in ProductViewModels)
        {
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
                _logger.LogError(ex, "Failed to load any image for product {ProductId}", product.Id);
                product.ThumbnailData = [];
            }
        }

        return Page();
    }
}