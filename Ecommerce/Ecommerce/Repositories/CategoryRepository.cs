using Ecommerce.Data;
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

        public async Task<Category> Add(Category category)
        {
             await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category;
            
        }

        public async Task<Category> Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
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

       
        public async Task<Category> Update(Category category)
        {
             _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
