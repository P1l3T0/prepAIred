using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for authentication and authorization operations.
    /// </summary>
    /// <remarks>
    /// This controller is responsible for handling HTTP requests related to authentication flows. It interacts 
    /// with the <see cref="IAuthRepository"/> for authentication operations and <see cref="IRefreshTokenRepository"/> for token management.
    /// </remarks>
    /// <param name="authRepository">Repository for handling authentication operations</param>
    /// <param name="refreshTokenRepository">Repository for managing refresh tokens</param>
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthRepository authRepository, IRefreshTokenRepository refreshTokenRepository) : Controller
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IRefreshTokenRepository _refreshTokenService = refreshTokenRepository;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                await _authRepository.RegisterAsync(registerDto);
                return Ok("Register successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                await _authRepository.LoginAsync(loginDto);
                return Ok("Login successful");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> GenerateNewRefreshToken()
        {
            try
            {
                RefreshTokenResponseDTO newRefreshToken = await _refreshTokenService.GenerateNewRefreshTokenAsync();
                return Ok(newRefreshToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authRepository.Logout();
                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}