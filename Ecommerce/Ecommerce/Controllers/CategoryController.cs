using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
     [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
   
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetAllCategories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            return Ok(categories);
        }

        [HttpGet("GetCategoryByID/{id}")]
        public async Task<IActionResult> GetCategoryByID(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound($"No category was found with ID: {id}");
            }

            return Ok(category);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryRepository.Add(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(ModelState);
        }
        
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
        {

            if (ModelState.IsValid)
            {
                var result = await _categoryRepository.Update(id , category);
                if (result.Success)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            return BadRequest(ModelState);
           

           
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryRepository.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
