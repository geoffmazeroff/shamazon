using Shamazon.Models;

namespace Shamazon.Interfaces;

/// <summary>
/// Represents a repository for products in the Shamazon system.
/// </summary>
public interface IProductRepository
{
    Task<List<ProductViewModel>> GetProductViewModelsAsync();
    
    Task<Product?> GetProductByIdAsync(int id);
}