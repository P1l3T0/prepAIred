using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API.Controllers
{
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
        public async Task<IActionResult> GetAiInterview()
        {
            List<InterviewDTO> interviews = await _aIRepository.GetAiInterview();
            return Ok(interviews);
        }
    }
}
