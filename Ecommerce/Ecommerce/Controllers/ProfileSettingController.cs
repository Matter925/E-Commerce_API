﻿using Ecommerce.Dto;
using Ecommerce.Dto.UserAuthDto;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSettingController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileSettingController(IUserService userService)
        {
            _userService = userService;
        }
        //[HttpGet("GetUserData/{email}")]
        //public async Task<IActionResult> GetUser(string email)
        //{
        //    var user = await _userService.GetUser(email);
        //    if (user.isNotNull)
        //    {
        //        return Ok(user);
        //    }
        //    return NotFound("Email is incorrect or not found !!");
        //}

        [HttpPost("ChangePassword/{email}")]
        public async Task<IActionResult> ChangePassword(string email, ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangePassword(email, model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("UpdateProfile/{email}")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto upProfile, string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateProfile(email, upProfile);
            if (result != null)
            {
                if (!result.IsAuthenticated)
                    return BadRequest(result.Message);
                return Ok(result);
            }
            return NotFound("Email is incorrect or not found !!");

        }
        [HttpGet("GetProfileToUpdate/{email}")]
        public async Task<IActionResult> GetProfileData(string email)
        {
            var result = await _userService.GetProfileData(email);
            if (result == null)
            {
                return BadRequest("Email is incorrect or not found !!!");
            }
            return Ok(result);

        }


        [HttpPost("ForgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound(email);
            }
            var result = await _userService.ForgotPasswordAsync(email);
            if (result.IsAuthenticated)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCodeDto codeDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.VerifyCodeAsync(codeDto);
                if (result)
                {
                    return Ok("Verify Code Successfully ");

                }
                return NotFound("Email or Verify Code is incorrect");
            }
            return BadRequest(ModelState);
        }

    }
}
