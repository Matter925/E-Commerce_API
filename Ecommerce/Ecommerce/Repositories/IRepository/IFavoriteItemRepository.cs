using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface IFavoriteItemRepository
    {
        Task<GeneralRetDto> AddItem(FavoriteItemAddDto dto);

        Task<GeneralRetDto> DeleteItem(int favoriteItemId);
        Task<GeneralRetDto> DeleteAll(int favoriteId);

        Task<IEnumerable<FavoriteItem>> GetItems(int favoriteId);
    }
}
