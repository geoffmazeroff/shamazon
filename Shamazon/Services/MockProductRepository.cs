using Shamazon.Interfaces;
using Shamazon.Models;

namespace Shamazon.Services;

public class MockProductRepository : IProductRepository
{
    public List<ProductViewModel> GetProductViewModels()
    {
        throw new NotImplementedException();
    }

    public Product GetProductById(int id)
    {
        throw new NotImplementedException();
    }
}