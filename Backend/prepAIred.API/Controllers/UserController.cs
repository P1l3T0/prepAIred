using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for managing user-related operations.
    /// </summary>
    /// <remarks>
    /// This controller is responsible for handling HTTP requests related to user data. It interacts
    /// with the <see cref="IUserRepository"/> to retrieve and manage user information.
    /// </remarks>
    /// <param name="userRepository">Repository for handling user-related operations</param>
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserRepository userRepository) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet("get-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (string.IsNullOrEmpty(Request.Cookies["AccessToken"]))
            {
                return NoContent();
            }

            CurrentUserDTO currentUser = await _userRepository.GetCurrentUserAsync();
            return Ok(currentUser);
        }
    }
}
