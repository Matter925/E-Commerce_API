using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<CartItem> AddItem(CartItemAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int cartItemId);
        Task<CartItem> GetItem(int cartItemId);
        Task<IEnumerable<CartItem>> GetItems(string userId);
    }
}
