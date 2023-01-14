using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteItemController : ControllerBase
    {
        private readonly IFavoriteItemRepository _favoriteItemRepository;
        private readonly IProductRepository _productRepository;

        public FavoriteItemController(IFavoriteItemRepository favoriteItemRepository, IProductRepository productRepository)
        {
            _favoriteItemRepository = favoriteItemRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        [Route("GetItems/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteItemDto>>> GetItems(string userId)
        {
            var favoriteItems = await _favoriteItemRepository.GetItems(userId);

            if (favoriteItems == null)
            {
                return NoContent();
            }

            var products = await _productRepository.GetProducts();

            return (from favoriteItem in favoriteItems
                    join product in products
                    on favoriteItem.ProductId equals product.Id
                    select new FavoriteItemDto
                    {
                        Id = favoriteItem.Id,
                        ProductId = favoriteItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        FavoriteId = favoriteItem.FavoriteId,

                    }).ToList();


        }

        [HttpPost("AddFavoriteItem")]
        public async Task<ActionResult<FavoriteItemDto>> AddItem([FromBody] FavoriteItemAddDto favoriteItemAddDto)
        {
            var newFavoriteItem = await _favoriteItemRepository.AddItem(favoriteItemAddDto);

            if (newFavoriteItem == null)
            {
                return NoContent();
            }

            var product = await _productRepository.GetById(newFavoriteItem.ProductId);

            if (product == null)
            {
                return BadRequest("Product Not Found");
            }

            return new FavoriteItemDto
            {
                Id = newFavoriteItem.Id,
                ProductId = newFavoriteItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                FavoriteId = newFavoriteItem.FavoriteId,
                
            };



        }


        [HttpDelete("DeleteItem/{favoriteItemId}")]
        public async Task<ActionResult<FavoriteItemDto>> DeleteItem(int favoriteItemId)
        {

            var favoriteItem = await _favoriteItemRepository.DeleteItem(favoriteItemId);

            if (favoriteItem == null)
            {
                return NotFound();
            }

            var product = await this._productRepository.GetById(favoriteItem.ProductId);

            if (product == null)
                return NotFound();

            return new FavoriteItemDto
            {
                Id = favoriteItem.Id,
                ProductId = favoriteItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                FavoriteId = favoriteItem.FavoriteId,


            };
        }

        [HttpDelete("DeleteAll/{favoriteId}")]
        public async Task<ActionResult<FavoriteItemDto>> DeleteAll(int favoriteId)
        {

            var favoriteItem = await _favoriteItemRepository.DeleteAll(favoriteId);

            if (favoriteItem == null)
            {
                return BadRequest("Wrong thing");


            }
            return Ok("Items successfully deleted");

        }

    }
}
