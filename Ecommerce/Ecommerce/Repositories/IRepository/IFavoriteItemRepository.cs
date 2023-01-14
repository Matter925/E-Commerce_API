using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface IFavoriteItemRepository
    {
        Task<FavoriteItem> AddItem(FavoriteItemAddDto favoriteItemAddDto);

        Task<FavoriteItem> DeleteItem(int favoriteItemId);
        Task<IEnumerable<FavoriteItem>> DeleteAll(int favoriteId);

        Task<IEnumerable<FavoriteItem>> GetItems(string userId);
    }
}
