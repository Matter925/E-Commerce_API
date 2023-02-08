using Ecommerce.Data;
using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.IRepository
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly ApplicationDbContext _context;


        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralRetDto> Add(CategoryDto dto)
        {
            var IsExist = await _context.Categories.Where(c => c.Name == dto.Name).FirstOrDefaultAsync();
            if(IsExist == null)
            {
                var category = new Category
                {
                    Name = dto.Name,
                    ImageURL = dto.ImageURL,
                    IsActive = dto.IsActive,
                };
                await _context.Categories.AddAsync(category);
                _context.SaveChanges();
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully"
                };
            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "The Category is already exist"
            };


        }

        public async Task<GeneralRetDto> Delete(int id)
        {
            var categoty = await _context.Categories.FindAsync(id);
            if (categoty == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No category was found with ID: {id}",
                };
            }
            _context.Remove(categoty);
            _context.SaveChanges();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted"
            };
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _context.Categories.OrderBy(o => o.Name).ToListAsync();
            return categories;
        }

       
        public async Task<GeneralRetDto> Update(int id , CategoryDto dto )
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No category was found with ID: {id}",
                };
            }
            category.Name = dto.Name;
            category.ImageURL = dto.ImageURL;
            category.IsActive = dto.IsActive;
            
            _context.Categories.Update(category);
            _context.SaveChanges(true);

            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Updated",
            };
        }
    }
}
