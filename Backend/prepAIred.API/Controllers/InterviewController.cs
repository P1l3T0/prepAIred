using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for managing interviews.
    /// </summary>
    /// <remarks>This controller serves as the API layer for interacting with AI interview data. It exposes
    /// endpoints for creating new AI interviews and retrieving existing ones. The controller depends on an 
    /// <see cref="IAIRepository"/> implementation to handle data operations.</remarks>
    /// <param name="aIRepository"></param>
    [ApiController]
    [Route("[controller]")]
    public class InterviewController(IInterviewRepository interviewRepository) : Controller
    {
        private readonly IInterviewRepository _interviewRepository = interviewRepository;

        [HttpPost("generate-ai-interviews")]
        public async Task<IActionResult> GenerateAiInterview([FromBody] AIRequestDTO aIRequest)
        {
            await _interviewRepository.CreateAiInterviews(aIRequest);
            return Ok("AI interviews created successfully.");
        }

        [HttpGet("get-ai-interviews")]
        public async Task<IActionResult> GetAiInterviews()
        {
            List<InterviewDTO> interviews = await _interviewRepository.GetAiInterview();
            return Ok(interviews);
        }
    }
}
