using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        
        Task<Category> GetById(int id);
        Task<Category> Add(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(Category category);



    }
}
