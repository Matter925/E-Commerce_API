﻿using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
   // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _userService.GetRoles();
            return Ok(result);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleDto assignRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AssignRole(assignRole);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);


        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateRole(createRole);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }



    }
}
