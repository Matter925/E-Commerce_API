using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductsByCatId(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategory(categoryId);


            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                ImageURL = dto.ImageURL ,
                CategoryId = dto.CategoryId,
            };
            _productRepository.Add(product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound($"No product was found with ID {id}");

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.ImageURL = dto.ImageURL;
            product.CategoryId = dto.CategoryId;
          

            _productRepository.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound($"No product was found with ID {id}");

            _productRepository.Delete(product);

            return Ok(product);
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
