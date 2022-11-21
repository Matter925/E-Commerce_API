using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Search( string Name);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
        Task<Product> GetById(int id);
        Task<Product> Add(Product product);
       Task<Product> Update(Product product);
        Task<Product> Delete(Product product);



    }
}
