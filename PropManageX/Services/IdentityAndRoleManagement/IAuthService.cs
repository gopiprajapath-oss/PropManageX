using PropManageX.DTOs.Auth;
using PropManageX.DTOs.OAuth;
using PropManageX.Models.Entities;

namespace PropManageX.Services.IdentityAndRoleManagement
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);

        Task<string> JWTTokenGenerator(UserModel user);
    }
}
