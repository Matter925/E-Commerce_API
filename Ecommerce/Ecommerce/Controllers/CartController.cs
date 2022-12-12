using Ecommerce.Dto;
using Ecommerce.Repositories;
using Ecommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    //[Authorize]
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
        [Authorize]
        [HttpGet]
        [Route("GetItems/{userId}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(string userId)
        {
            var cartItems = await _cartRepository.GetItems(userId);

            if (cartItems == null)
            {
                return NoContent();
            }

            var products = await _productRepository.GetProducts();

            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qty = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    }).ToList();


        }
        [Authorize]
        [HttpGet("GetItem/{cartItemId}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int cartItemId)
        {

            var cartItem = await _cartRepository.GetItem(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetById(cartItem.ProductId);

            if (product == null)
            {
                return NotFound();
            }
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty
            };


        }
        [Authorize]
        [HttpPost("AddItem")]
        public async Task<ActionResult<CartItemDto>> AddItem([FromBody] CartItemAddDto cartItemAddDto)
        {
           var newCartItem = await _cartRepository.AddItem(cartItemAddDto);

            if (newCartItem == null)
            {
                return NoContent();
            }

            var product = await _productRepository.GetById(newCartItem.ProductId);

            if (product == null)
            {
            return BadRequest("Product Not Found");
            }

            return new CartItemDto
            {
                Id = newCartItem.Id,
                ProductId = newCartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = newCartItem.CartId,
                Qty = newCartItem.Qty,
                TotalPrice = product.Price * newCartItem.Qty
            };



        }
        [Authorize]
        [HttpDelete("DeleteItem/{cartItemId}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int cartItemId)

        {

            var cartItem = await _cartRepository.DeleteItem(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            var product = await this._productRepository.GetById(cartItem.ProductId);

            if (product == null)
                return NotFound();

            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty

            };
        }
         [HttpDelete("DeleteAll/{cartId}")]
        public async Task<ActionResult<CartItemDto>> DeleteAll(int cartId)
        {

                var cartItem = await _cartRepository.DeleteAll(cartId);

                if (cartItem == null)
                {
                return BadRequest("Wrong thing");
                
               
                }
            return Ok("Items successfully deleted");

        }
        [Authorize]
        [HttpPut("UpdateQtyItem")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
           
                var cartItem = await _cartRepository.UpdateQty(cartItemQtyUpdateDto);
                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await _productRepository.GetById(cartItem.ProductId);

            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty

            };



        }

    }
}
