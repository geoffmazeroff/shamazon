using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shamazon.Pages;

public class ProductDetails : PageModel
{
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        
        // Get the product from the repository

        return Page();
    }
}