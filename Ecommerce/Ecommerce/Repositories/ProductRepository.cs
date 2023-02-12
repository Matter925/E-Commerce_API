using Ecommerce.Data;
using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
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

        

        public async Task<Product> GetById(int id)
        {
            var product =await _context.Products.Include(m => m.Category).SingleOrDefaultAsync(m=>m.Id == id);
            if(product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .OrderByDescending(m => m.Price)
               .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryID(int categoryId)
        {
            return await _context.Products.Where(e => e.CategoryId == categoryId).Include(c=>c.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> Search(string Name)
        {
            IQueryable<Product> query = _context.Products;
            if(!string.IsNullOrEmpty(Name))
            {
                query = query.Where(e => e.Name.Contains(Name)).OrderByDescending(m => m.Price)
               .Include(m => m.Category);
            }
            return await query.ToListAsync();
        }
        public async Task<GeneralRetDto> Add(ProductDto dto)
        {
            var IsExist = await _context.Products.Where(p => p.Name == dto.Name).SingleOrDefaultAsync();
            if (IsExist == null)
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    ImageURL = dto.ImageURL,
                    CategoryId = dto.CategoryId,

                };
                await _context.Products.AddAsync(product);
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
                Message = "The Product is already exist"
            };
        }

        public async Task<GeneralRetDto> Delete(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No product was found with ID: {id}",
                };
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted",
            };
        }

        public async Task<GeneralRetDto> Update( int id ,ProductDto dto)
        {
            var product = await _context.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = $"No Product was found with ID: {id}",
                };
            }
            product.Name = dto.Name;
            product.ImageURL = dto.ImageURL;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            _context.Products.Update(product);
            _context.SaveChanges();

            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Updated",
            };
        }
    }
}
