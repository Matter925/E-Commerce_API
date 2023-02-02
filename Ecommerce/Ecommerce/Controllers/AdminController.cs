using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("CreateRole")]
        //public async Task<IActionResult> CreateRole(CreateRoleDto createRole)
        //{

        //    var result = await _roleManager.CreateAsync(new IdentityRole
        //    {

        //        Name = createRole.RoleName
        //    }) ;

        //    if (result.Succeeded)
        //    {
        //        return Ok("New Role Created");
        //    }
        //    else
        //    {
        //        return BadRequest(result.Errors);
        //    }
        //}
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleDto assignRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AssignRole(assignRole);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(assignRole);


        }
        

        
    }
}
