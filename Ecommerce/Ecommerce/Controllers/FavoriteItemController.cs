using Ecommerce.Dto;
using Ecommerce.Models;
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
        [Route("GetItems/{favoriteId}")]
        public async Task<ActionResult> GetItems(int favoriteId)
        {
            var favoriteItems = await _favoriteItemRepository.GetItems(favoriteId);

            if (favoriteItems == null)
            {
                return NoContent();
            }

            return Ok(favoriteItems);

        }

        [HttpPost("AddFavoriteItem")]
        public async Task<ActionResult> AddItem([FromBody] FavoriteItemAddDto dto)
        {
            if (ModelState.IsValid)
            {
                var newFavoriteItem = await _favoriteItemRepository.AddItem(dto);

                if (newFavoriteItem.Success)
                {
                    return Ok(newFavoriteItem);
                }
                return BadRequest(newFavoriteItem);
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("DeleteItem/{favoriteItemId}")]
        public async Task<ActionResult<FavoriteItemDto>> DeleteItem(int favoriteItemId)
        {
            var result = await _favoriteItemRepository.DeleteItem(favoriteItemId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpDelete("DeleteAll/{favoriteId}")]
        public async Task<ActionResult<FavoriteItemDto>> DeleteAll(int favoriteId)
        {
            var result = await _favoriteItemRepository.DeleteAll(favoriteId);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);

        }
    }
}
