using Ecommerce.Data;
using Ecommerce.Dto;
using Ecommerce.Dto.CartDto;

using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;
using Ecommerce.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _context.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<GeneralRetDto> AddItem(CartItemAddDto dto)
        {
            if (await CartItemExists(dto.CartId, dto.ProductId) == false)
            {
                var item = new CartItem
                {
                    CartId = dto.CartId,
                    ProductId = dto.ProductId,
                    Qty = dto.Qty
                };
                
                await _context.CartItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return new GeneralRetDto { 
                    Success =true,
                    Message ="Successfully Add"
                };  
            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "Item already add"
            }; 
        }

        public async Task<GeneralRetDto> DeleteItem(int ItemId)
        {
            var item = await _context.CartItems.FindAsync(ItemId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully Deleted"
                };
            }

            return new GeneralRetDto { 
                Success =false,
                Message = "Item Id is not found !!"
            };
        }

        public async Task<CartItem> GetItem(int ItemId)
        {
            var IsExist = await _context.CartItems.FindAsync(ItemId);
            if (IsExist == null)
            {
                return null;
            }
            var Item = await _context.CartItems.Include(c => c.Product).SingleOrDefaultAsync(r => r.Id == ItemId);
            return Item;
        }

        public async Task<ItemsCartDto> GetItems(int CartId)
        {
            double total = 0;
            var IsExist = await _context.Carts.FindAsync(CartId);
            if (IsExist == null)
            {
                return null;
            }
            var Items = await _context.CartItems.Include(c => c.Product).Include(c => c.Product.Category).Where(o => o.CartId == CartId).ToListAsync();
            foreach (var Item in Items)
            {
                total += Item.Product.Price*(Item.Qty);
            };
            return new ItemsCartDto
            {
                Items = Items,
                Count = Items.Count(),
                Total = total,

            };
        }

        public async Task<GeneralRetDto> UpdateQty(CartItemQtyUpdateDto dto)
        {
            var item = await _context.CartItems.FindAsync(dto.CartItemId);

            if (item != null)
            {
                item.Qty = dto.Qty;
                await _context.SaveChangesAsync();
                return new GeneralRetDto
                {
                    Success =true,
                    Message= "Successfully Updated"
                };
            }

            return new GeneralRetDto
            {
                Success = false,
                Message = "Item Id is not found !!"
            };
        }

        public async Task<GeneralRetDto> DeleteAll(int cartId)
        {
            var Exist = await _context.Carts.FindAsync(cartId);
            if (Exist == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "Cart Id is not found !!"
                };
            }
            var items = await _context.CartItems.Where(e => e.CartId == cartId).ToListAsync();

            foreach (var ex in items)
            {
                _context.CartItems.Remove(ex);
            }
            await _context.SaveChangesAsync();
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted Items"
            };

        }
    }
}
