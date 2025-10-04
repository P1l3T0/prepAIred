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

        [HttpGet("get-latest-hr-interviews")]
        public async Task<IActionResult> GetLatestHrInterview()
        {
            List<HRInterviewDTO> hrInterviews = await _interviewRepository.GetLatestInterviews<HRInterview, HRInterviewDTO>();
            return Ok(hrInterviews);
        }

        [HttpPost("evaluate-hr-interviews")]
        public async Task<IActionResult> EvaluateHrInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            await _interviewRepository.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);
            return Ok("HR interviews evaluated successfully.");
        }

        [HttpPost("generate-technical-interviews")]
        public async Task<IActionResult> GenerateTechnicalInterviews([FromBody] TechnicalRequestDTO technicalRequest)
        {
            await _interviewRepository.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest);
            return Ok("Technical interviews created successfully.");
        }

        [HttpGet("get-latest-technical-interviews")]
        public async Task<IActionResult> GetLatestTechnicalInterview()
        {
            List<TechnicalInterviewDTO> technicalInterviews = await _interviewRepository.GetLatestInterviews<TechnicalInterview, TechnicalInterviewDTO>();
            return Ok(technicalInterviews);
        }

        [HttpPost("evaluate-technical-interviews")]
        public async Task<IActionResult> EvaluateTechnicalInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            await _interviewRepository.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests);
            return Ok("Technical interviews evaluated successfully.");
        }
    }
}