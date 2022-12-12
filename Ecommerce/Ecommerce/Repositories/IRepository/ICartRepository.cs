using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<CartItem> AddItem(CartItemAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty( CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int cartItemId);
        Task<IEnumerable<CartItem>> DeleteAll(int cartId);
        Task<CartItem> GetItem(int cartItemId);
        Task<IEnumerable<CartItem>> GetItems(string userId);
    }
}
