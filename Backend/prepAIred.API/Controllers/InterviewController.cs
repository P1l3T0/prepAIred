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
    /// <see cref="IInterviewService"/> implementation to handle interview data operations.</remarks>
    /// <param name="interviewService">Repository for handling interview operations</param>
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController(IInterviewService interviewService) : Controller
    {
        private readonly IInterviewService _interviewService = interviewService;

        [HttpPost("generate-hr-interviews")]
        public async Task<IActionResult> GenerateHrInterview([FromBody] HrRequestDTO hrRequest)
        {
            await _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest);
            return Ok("HR interviews created successfully.");
        }

        [HttpGet("get-latest-hr-interviews")]
        public async Task<IActionResult> GetLatestHrInterview()
        {
            List<HRInterviewDTO> hrInterviews = await _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>();
            return Ok(hrInterviews);
        }

        [HttpPost("evaluate-hr-interviews")]
        public async Task<IActionResult> EvaluateHrInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            await _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);
            return Ok("HR interviews evaluated successfully.");
        }

        [HttpPost("generate-technical-interviews")]
        public async Task<IActionResult> GenerateTechnicalInterviews([FromBody] TechnicalRequestDTO technicalRequest)
        {
            await _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest);
            return Ok("Technical interviews created successfully.");
        }

        [HttpGet("get-latest-technical-interviews")]
        public async Task<IActionResult> GetLatestTechnicalInterview()
        {
            List<TechnicalInterviewDTO> technicalInterviews = await _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>();
            return Ok(technicalInterviews);
        }

        [HttpPost("evaluate-technical-interviews")]
        public async Task<IActionResult> EvaluateTechnicalInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            await _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests);
            return Ok("Technical interviews evaluated successfully.");
        }
    }
}