using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        
        Task<Category> GetById(int id);
        Task<GeneralRetDto> Add(CategoryDto dto);
        Task<GeneralRetDto> Update(int id , CategoryDto category);
        Task<GeneralRetDto> Delete(int id);



    }
}
