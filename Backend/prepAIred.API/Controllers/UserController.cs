using prepAIred.Data;
using prepAIred.Services;
using prepAIred.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for managing user-related operations.
    /// </summary>
    /// <remarks>
    /// This controller is responsible for handling HTTP requests related to user data. It interacts
    /// with the <see cref="IUserService"/> to retrieve and manage user information.
    /// </remarks>
    /// <param name="userService">Repository for handling user-related operations</param>
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Cookies["AccessToken"]))
                {
                    return NoContent();
                }

                CurrentUserDTO currentUser = await _userService.GetCurrentUserAsync();
                return Ok(currentUser);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            try
            {
                await _userService.UpdateCurrentUserAsync(userCredentialsDto);
                return Ok("User updated");
            }
            catch (DataValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            try
            {
                await _userService.DeleteCurrentUserAsync();
                return Ok("User deleted");
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
