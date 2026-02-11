using CMaaS.Backend.Dtos;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMaaS.Backend.Controllers
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

        /// <summary>
        /// Register a new company with an admin user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>Registration result with tenant ID and API key</returns>
        [HttpPost("register-company")]
        public async Task<IActionResult> RegisterCompany(RegisterRequestDto request)
        {
            var result = await _authService.RegisterCompanyAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Authenticate user and get JWT token
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(new { token = result.Data });
        }
    }
}