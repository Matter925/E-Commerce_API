using Ecommerce.Dto;
using Ecommerce.Dto.CartDto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<GeneralRetDto> AddItem(CartItemAddDto cartItemToAddDto);
        Task<GeneralRetDto> UpdateQty( CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<GeneralRetDto> DeleteItem(int ItemId);
        Task<GeneralRetDto> DeleteAll(int cartId);
        Task<CartItem> GetItem(int ItemId);
        Task<ItemsCartDto> GetItems(int CartId);
    }
}
