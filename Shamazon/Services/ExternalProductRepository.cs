using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

public class ExternalProductRepository : IProductRepository
{
    public Task<List<ProductViewModel>> GetProductViewModelsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}