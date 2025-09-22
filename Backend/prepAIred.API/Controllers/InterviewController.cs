using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for managing interviews.
    /// </summary>
    /// <remarks>This controller serves as the API layer for interacting with interview data. It exposes
    /// endpoints for creating new AI interviews and retrieving existing ones. The controller depends on an 
    /// <see cref="IInterviewRepository"/> implementation to handle interview data operations.</remarks>
    /// <param name="interviewRepository">Repository for handling interview operations</param>
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController(IInterviewRepository interviewRepository) : Controller
    {
        private readonly IInterviewRepository _interviewRepository = interviewRepository;

        [HttpPost("generate-hr-interviews")]
        public async Task<IActionResult> GenerateHrInterview([FromBody] HrRequestDTO hrRequest)
        {
            await _interviewRepository.GenerateInterviewsAsync<HRInterview>(hrRequest);
            return Ok("HR interviews created successfully.");
        }

        [HttpPost("generate-technical-interviews")]
        public async Task<IActionResult> GenerateTechnicalInterviews([FromBody] TechnicalRequestDTO technicalRequest)
        {
            await _interviewRepository.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest);
            return Ok("Technical interviews created successfully.");
        }
    }
}