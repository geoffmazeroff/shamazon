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
    }
    
    public Product Product { get; set; }
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        
        // Get the product from the repository
        var retrievedProduct = await _productRepository.GetProductByIdAsync(id.Value);
        if (retrievedProduct is null)
        {
            return NotFound();
        }
        
        Product = retrievedProduct;
        return Page();
    }
}