using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilePictureController(IProfilePictureRepository profilePictureRepository) : Controller
    {
        private readonly IProfilePictureRepository _profilePictureRepository = profilePictureRepository;

        [HttpGet("get-profile-picture-url")]
        public async Task<IActionResult> GetProfilePicture()
        {
            string profilePictureUrl = await _profilePictureRepository.GetProfilePictureUrlAsync();

            return Ok(profilePictureUrl);
        }

        [HttpPost("change-profile-picture")]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ProfilePictureDTO profilePictureDTO)
        {
            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDTO);
            return Ok("Profile picture changed");
        }
    }
}
