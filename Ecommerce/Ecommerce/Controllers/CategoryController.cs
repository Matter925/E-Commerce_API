using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var category = new Category { Name = dto.Name };

            await _categoryRepository.Add(category);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return NotFound($"No category was found with ID: {id}");

            category.Name = dto.Name;

            _categoryRepository.Update(category);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return NotFound($"No category was found with ID: {id}");

            _categoryRepository.Delete(category);

            return Ok(category);
        }
    }
}
