using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            bool response = await _authService.RegisterAsync(registerDto);
            if (response)
            {
                return Ok(registerDto);
            }
            else
            {
                return BadRequest("User has already registered");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);
        }
    }
}
