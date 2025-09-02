using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API.Controllers
{
    [Route("[controller]")]
    public class UserController(IUserRepository userRepository) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet("get-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                CurrentUserDTO currentUser = await _userRepository.GetCurrentUserAsync();
                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
