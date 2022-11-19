using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> ChangePassword(ChangePasswordDto model);
        Task<string> AssignRole(AssignRoleDto assignRole);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}