using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing AI interviews.
    /// </summary>
    /// <remarks>This controller serves as the API layer for interacting with AI interview data. It exposes
    /// endpoints for creating new AI interviews and retrieving existing ones. The controller depends on an 
    /// <see cref="IAIRepository"/> implementation to handle data operations.</remarks>
    /// <param name="aIRepository"></param>
    [ApiController]
    [Route("[controller]")]
    public class AIController(IAIRepository aIRepository) : Controller
    {
        private readonly IAIRepository _aIRepository = aIRepository;

        [HttpPost("create-ai-interviews")]
        public async Task<IActionResult> CreateAiInterview([FromBody] AIRequestDTO aIRequest)
        {
            await _aIRepository.CreateAiInterview(aIRequest);
            return Ok("AI interviews created successfully.");
        }

        [HttpGet("get-ai-interviews")]
        public async Task<IActionResult> GetAiInterviews()
        {
            List<InterviewDTO> interviews = await _aIRepository.GetAiInterview();
            return Ok(interviews);
        }
    }
}
