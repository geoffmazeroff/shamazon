using Shamazon.Models;

namespace Shamazon.Interfaces;

/// <summary>
/// Represents a repository for products in the Shamazon system.
/// </summary>
public interface IProductRepository
{
    List<ProductViewModel> GetProductViewModels();
    
    Product GetProductById(int id);
}