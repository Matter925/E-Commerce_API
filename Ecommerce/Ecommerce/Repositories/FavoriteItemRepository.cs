using Ecommerce.Data;
using Ecommerce.Dto;
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

        public async Task<FavoriteItem> AddItem(FavoriteItemAddDto favoriteItemAddDto)
        {
            if (await FavoriteItemExists(favoriteItemAddDto.FavoriteId, favoriteItemAddDto.ProductId) == false)
            {
                var item = await(from product in _context.Products
                                 where product.Id == favoriteItemAddDto.ProductId
                                 select new FavoriteItem
                                 {
                                     FavoriteId = favoriteItemAddDto.FavoriteId,
                                     ProductId = product.Id,
                                     
                                 }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await _context.FavoriteItems.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        private async Task<bool> FavoriteItemExists(int favoriteId, int productId)
         { 
            return await _context.FavoriteItems.AnyAsync(c =>c.FavoriteId ==favoriteId &&c.ProductId == productId);

        }

        public async Task<IEnumerable<FavoriteItem>> DeleteAll(int favoriteId)
        {
            var item = await _context.FavoriteItems.Where(e => e.FavoriteId == favoriteId).ToListAsync();

            if (item.Any())
            {
                foreach (var ex in item)
                {
                    _context.FavoriteItems.Remove(ex);
                }
                await _context.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async Task<FavoriteItem> DeleteItem(int favoriteItemId)
        {
            var item = await _context.FavoriteItems.FindAsync(favoriteItemId);

            if (item != null)
            {
                _context.FavoriteItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return item;
        }

        public async Task<IEnumerable<FavoriteItem>> GetItems(string userId)
        {
            return await (from favorite in _context.Favorites
                          join favoriteItem in _context.FavoriteItems
                          on favorite.Id equals favoriteItem.FavoriteId
                          where favorite.UserId == userId
                          select new FavoriteItem
                          {
                              Id = favoriteItem.Id,
                              ProductId = favoriteItem.ProductId,
                              FavoriteId = favoriteItem.FavoriteId
                          }).ToListAsync();
        }






    }
}
