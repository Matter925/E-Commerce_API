using System.Threading.Tasks;

using Ecommerce.Dto;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Dto.UserAuthDto;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);

        //-----------------------------------------------------------------------------------------
        Task<UpdateProfileDto> GetProfileData(string email);
        Task<AuthModel> UpdateProfile(string email, UpdateProfileDto upProfile);
        Task<GetUserDto> GetUser(string email);
        Task<AuthModel> ChangePassword(string email, ChangePasswordDto model);
        Task<GeneralRetDto> ForgotPasswordAsync(string email);
        Task<RestTokenDto> VerifyCodeAsync(VerifyCodeDto codeDto);
        Task<GeneralRetDto> CreateNewPassword(string email, CreatePasswordDto model);

        //----------------------------------------------------------------------------------------------------------

        Task<GeneralRetDto> AssignRole(AssignRoleDto assignRole);
        Task<GeneralRetDto> CreateRole(CreateRoleDto createRole);
        Task<List<IdentityRole>> GetRoles();
        
        
    }
}