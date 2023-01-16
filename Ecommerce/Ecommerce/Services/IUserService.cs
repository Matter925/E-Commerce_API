using System.Threading.Tasks;
using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> UpdateProfile(string email, UpdateProfileDto upProfile);
        Task<GetUserDto> GetUser(string email);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> ChangePassword(ChangePasswordDto model);
        Task<string> AssignRole(AssignRoleDto assignRole);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}