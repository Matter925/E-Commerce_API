using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();

            return product;
        }

        public async Task<Product> Delete(Product product)
        {
             _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(m => m.Category).SingleOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .OrderByDescending(m => m.Price)
               .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return await _context.Products.Where(e => e.CategoryId == categoryId).Include(c=>c.Category).ToListAsync();
        }

        public async Task<Product> Update(Product product)
        {
             _context.Products.Update(product);
            _context.SaveChanges();

            return product;
        }
    }
}
