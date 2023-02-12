using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;

namespace Ecommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Search( string Name);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetByCategoryID(int categoryId);
        Task<Product> GetById(int id);
        Task<GeneralRetDto> Add(ProductDto dto);
       Task<GeneralRetDto> Update(int id, ProductDto dto);
        Task<GeneralRetDto> Delete(int id);



    }
}
