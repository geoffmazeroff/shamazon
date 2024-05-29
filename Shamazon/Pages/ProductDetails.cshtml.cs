using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Pages;

public class ProductDetails : PageModel
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductDetails> _logger;
    
    public ProductDetails(ILogger<ProductDetails> logger, IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _logger = logger;

        Product = new Product();
    }
    
    public Product? Product { get; set; }
    public int? Id { get; set; }
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Id = id;
        if (id is null)
        {
            _logger.LogInformation("ProductDetails.OnGetAsync() - No product ID was provided in the query string");
            
            Product = null;
            return Page();
        }
        
        // Get the product from the repository.
        // Note: Null product condition is handled via the Razor page with an alert.
        try
        {
            Product = await _productRepository.GetProductByIdAsync(id.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ProductDetails.OnGetAsync() - Failed to retrieve product {ProductId} from the repository", id);
            return RedirectToPage("/Error");
        }
        return Page();
    }
}