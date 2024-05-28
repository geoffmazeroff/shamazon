using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Pages;

public class ProductDetails : PageModel
{
    private readonly IProductRepository _productRepository;
    
    public ProductDetails(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        Product = new Product();
    }
    
    public Product? Product { get; set; }
    public int? Id { get; set; }
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Id = id;
        if (id is null)
        {
            Product = null;
            return Page();
        }
        
        // Get the product from the repository.
        // Note: Null product condition is handled via the Razor page.
        Product = await _productRepository.GetProductByIdAsync(id.Value);
        return Page();
    }
}