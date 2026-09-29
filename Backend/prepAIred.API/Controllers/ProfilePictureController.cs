using prepAIred.Data;
using prepAIred.Services;
using prepAIred.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    [ApiController]
    [Route("api/profile-pictures")]
    public class ProfilePictureController(IProfilePictureService profilePictureService) : Controller
    {
        private readonly IProfilePictureService _profilePictureService = profilePictureService;

        [HttpGet]
        public async Task<IActionResult> GetProfilePicture()
        {
            try
            {
                string profilePictureUrl = await _profilePictureService.GetProfilePictureUrlAsync();
                return Ok(profilePictureUrl);
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

        [HttpPut]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ProfilePictureDTO profilePictureDTO)
        {
            try
            {
                await _profilePictureService.ChangeProfilePictureAsync(profilePictureDTO);
                return Ok("Profile picture changed");
            }
            catch (UnsupportedFileExtensionException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProfilePictureException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
