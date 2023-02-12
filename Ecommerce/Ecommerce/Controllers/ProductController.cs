using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetProducts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProductByID{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound($"No product was found with ID {id} ");

            return Ok(product);
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var products = await _productRepository.GetByCategoryID(categoryId);

            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productRepository.Add(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _productRepository.Update(id, dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeletProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productRepository.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("Search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var products = await _productRepository.Search(name);
            if(products.Any())
            {
                return Ok(products);
            }
            return NotFound();

        }


    }
}
