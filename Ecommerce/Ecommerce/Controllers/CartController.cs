using Ecommerce.Dto;
using Ecommerce.Repositories;
using Ecommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
       
        [HttpGet("GetItems/{CartId}")]
        public async Task<ActionResult> GetItems(int CartId)
        {
            var cartItems = await _cartRepository.GetItems(CartId);
            if (cartItems == null)
            {
                return NoContent();
            }
            return Ok(cartItems);
        }
        
        [HttpGet("GetItem/{ItemId}")]
        public async Task<ActionResult> GetItem(int ItemId)
        {
            var cartItem = await _cartRepository.GetItem(ItemId);
            if (cartItem == null)
            {
                return NotFound("The Item Id Not Found !! ");
            }
            return Ok(cartItem);
        }
        
        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem([FromBody] CartItemAddDto cartItemAddDto)
        {
            if(ModelState.IsValid)
            {
                var newCartItem = await _cartRepository.AddItem(cartItemAddDto);

                if (newCartItem.Success)
                {
                    return Ok(newCartItem);
                }
                return BadRequest(newCartItem);
            }
            return BadRequest(ModelState);
        }
        
        [HttpDelete("DeleteItem/{ItemId}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int ItemId)
        {
            var result = await _cartRepository.DeleteItem(ItemId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);

        }
         [HttpDelete("DeleteAll/{cartId}")]
        public async Task<ActionResult> DeleteAll(int cartId)
        {

          var result = await _cartRepository.DeleteAll(cartId);

            if(result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        
        [HttpPut("UpdateQtyItem")]
        public async Task<ActionResult> UpdateQty(CartItemQtyUpdateDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _cartRepository.UpdateQty(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            return BadRequest(ModelState);
        }

    }
}
