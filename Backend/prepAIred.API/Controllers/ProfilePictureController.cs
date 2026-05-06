using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilePictureController(IProfilePictureService profilePictureService) : Controller
    {
        private readonly IProfilePictureService _profilePictureService = profilePictureService;

        [HttpGet("get-profile-picture-url")]
        public async Task<IActionResult> GetProfilePicture()
        {
            try
            {
                string profilePictureUrl = await _profilePictureService.GetProfilePictureUrlAsync();
                return Ok(profilePictureUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("change-profile-picture")]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ProfilePictureDTO profilePictureDTO)
        {
            try
            {
                await _profilePictureService.ChangeProfilePictureAsync(profilePictureDTO);
                return Ok("Profile picture changed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
