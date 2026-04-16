using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.OAuth;
using PropManageX.Services.IdentityAndRoleManagement;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        

        //---------------Register---------------------------

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (result == "User already exists")
                return BadRequest(result);
            return Ok(new { message = result });
        }
        

        //---------------Login---------------------------

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)

        {

        

            var token = await _authService.Login(loginDto);

            if (token == null)

            {

                return Unauthorized("Invalid email or password.");

            }

            // Return the token as a JSON object

            return Ok(new { token = token });

        }


        // GET: api/Auth/users
        [HttpGet("users")]
        // [Authorize(Roles = "Admin")] // Uncomment this to lock it down to Admins only!
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        // DELETE: api/Auth/users/{id}
        [HttpDelete("users/{id}")]
        // [Authorize(Roles = "Admin")] // Uncomment this to lock it down to Admins only!
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _authService.DeleteUserAsync(id);

            if (!success)
            {
                return NotFound("User not found or already deleted.");
            }

            // NOTE: Returning Ok("string") is crucial here because your Angular 
            // service uses { responseType: 'text' }. If you return an object, 
            // Angular will try to parse it as JSON and throw an error even if the deletion worked.
            return Ok("User successfully deleted.");
        }


    }
}