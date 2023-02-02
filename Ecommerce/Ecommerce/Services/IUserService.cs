using System.Threading.Tasks;
using Ecommerce.Dto;
using Ecommerce.Dto.UserAuthDto;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> UpdateProfile(string email, UpdateProfileDto upProfile);
        Task<UpdateProfileDto> GetProfileData(string email);
        Task<GetUserDto> GetUser(string email);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> ChangePassword(string email, ChangePasswordDto model);
        Task<AuthModel> ForgotPasswordAsync(string email);
        Task<bool> VerifyCodeAsync(VerifyCodeDto codeDto);
        Task<string> AssignRole(AssignRoleDto assignRole);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        
    }
}