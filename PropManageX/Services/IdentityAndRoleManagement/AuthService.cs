using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PropManageX.DTOs.Auth;
using PropManageX.DTOs.OAuth;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PropManageX.Services.IdentityAndRoleManagement
{
    public class AuthService : IAuthService
    {
        private readonly  PropManageXContext _context ;
        public AuthService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
                return "User not found ";

            if (user.Password != HashPassword(loginDto.Password))
                return "Invalid password";

            return await JWTTokenGenerator(user).ConfigureAwait(false);
        }

       

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }


        public async Task<string> Register(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email);

            if (existingUser != null)
                return "User already exists";

            var user = new UserModel
            {
                Name = registerDto.Name,
                Role = registerDto.Role,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Password = HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";

        }




        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // Maps your database entity to the DTO expected by Angular
            return await _context.Users
                .Select(user => new UserDto
                {
                    UserID = user.UserID, // Assuming your DB Primary Key is 'Id'
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Phone = user.Phone // Adjust depending on your DB column name
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return false; // User doesn't exist
            }

            // Optional: Check if you are trying to delete the last admin, etc.

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        //------------JWT Token Generator Method-----------------
        public async Task<string> JWTTokenGenerator(UserModel user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("ThisIsASuperLongSecretKeyForJwtAuthentication1234567890!")); // match appsettings.json

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new Claim(ClaimTypes.Name , user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                issuer: "PropManageX",
                audience: "PropManageXUsers",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

   
    }
}
