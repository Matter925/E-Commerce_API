using Ecommerce.Data;
using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;
using Ecommerce.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    
    public class FavoriteItemRepository : IFavoriteItemRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralRetDto> AddItem(FavoriteItemAddDto dto)
        {
            if (await FavoriteItemExists(dto.FavoriteId, dto.ProductId) == false)
            {
                var item = new FavoriteItem
                {
                    FavoriteId = dto.FavoriteId,
                    ProductId = dto.ProductId,

                };
                
                await _context.FavoriteItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully Add"
                };
            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "Item already add"
            };
            ;
        }

        private async Task<bool> FavoriteItemExists(int favoriteId, int productId)
         { 
            return await _context.FavoriteItems.AnyAsync(c =>c.FavoriteId ==favoriteId &&c.ProductId == productId);

        }

        public async Task<GeneralRetDto> DeleteAll(int favoriteId)
        {
            var Exist = await _context.Favorites.FindAsync(favoriteId);
            if (Exist == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "Favorite Id is not found !!"
                };
            }
            var item = await _context.FavoriteItems.Where(e => e.FavoriteId == favoriteId).ToListAsync();

            foreach (var ex in item)
            {
                _context.FavoriteItems.Remove(ex);
            }
            await _context.SaveChangesAsync();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted Items"
            };

        }

        public async Task<GeneralRetDto> DeleteItem(int favoriteItemId)
        {
            var item = await _context.FavoriteItems.FindAsync(favoriteItemId);

            if (item != null)
            {
                _context.FavoriteItems.Remove(item);
                await _context.SaveChangesAsync();
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully Deleted Item"
                };
            }

            return new GeneralRetDto
            {
                Success = false,
                Message = "Item Id is not found !!"
            };
        }

        public async Task<IEnumerable<FavoriteItem>> GetItems(int favoriteId)
        {
            var IsExist = await _context.Favorites.FindAsync(favoriteId);
            if (IsExist != null)
            {
                var Items = await _context.FavoriteItems.Include(c => c.Product).Where(o => o.FavoriteId == favoriteId).ToListAsync();
                return Items;
            }
            return null;
            
        }

    }
}
