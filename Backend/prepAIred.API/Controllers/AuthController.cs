using prepAIred.Data;
using prepAIred.Services;
using prepAIred.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for authentication and authorization operations.
    /// </summary>
    /// <remarks>
    /// This controller is responsible for handling HTTP requests related to authentication flows. It interacts 
    /// with the <see cref="IAuthService"/> for authentication operations and <see cref="IRefreshTokenService"/> for token management.
    /// </remarks>
    /// <param name="authService">Repository for handling authentication operations</param>
    /// <param name="refreshTokenService">Repository for managing refresh tokens</param>
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService, IRefreshTokenService refreshTokenService) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            try
            {
                await _authService.RegisterAsync(userCredentialsDto);
                return Ok("Register successful");
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EmptyFieldsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                await _authService.LoginAsync(loginDto);
                return Ok("Login successful");
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EmptyFieldsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (InvalidRefreshTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidAccessTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NoUserLoggedInException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                return Ok("Logged out successfully");
            }
            catch (NoUserLoggedInException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}