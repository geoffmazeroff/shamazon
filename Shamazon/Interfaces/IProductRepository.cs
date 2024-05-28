using Shamazon.Models;

namespace Shamazon.Interfaces;

/// <summary>
/// Represents a repository for products in the Shamazon system.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets a list of product view models.
    /// </summary>
    /// <param name="search">Optional text to find in the product description (not case-sensitive).</param>
    /// <remarks>A product view model is a limited set of product information to drive a list view.</remarks>
    /// <returns>The list of product view models; empty if no models were found.</returns>
    Task<List<ProductViewModel>> GetProductViewModelsAsync(string search = "");
    
    /// <summary>
    /// Gets a product by its ID.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>The product with the given ID; null if the product could not be found.</returns>
    Task<Product?> GetProductByIdAsync(int id);
}