using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductRepository _productRepository;
    
    public List<ProductViewModel> ProductViewModels { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ProductViewModels = await _productRepository.GetProductViewModelsAsync();
        return Page();
    }
}