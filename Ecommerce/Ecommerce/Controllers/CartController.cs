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

        [HttpGet("{cartItemId:int}")]
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

        [HttpPost]
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
        [HttpDelete("{cartItemId:int}")]
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

        [HttpPut("{cartItemId:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int cartItemId, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
           
                var cartItem = await _cartRepository.UpdateQty(cartItemId, cartItemQtyUpdateDto);
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
