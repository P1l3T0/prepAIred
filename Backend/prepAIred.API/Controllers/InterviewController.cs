using prepAIred.Data;
using prepAIred.Services;
using prepAIred.Exceptions;
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
    [Route("api/interviews")]
    public class InterviewController(IInterviewService interviewService) : Controller
    {
        private readonly IInterviewService _interviewService = interviewService;

        [HttpPost("hr")]
        public async Task<IActionResult> GenerateHrInterview([FromBody] HrRequestDTO hrRequest)
        {
            try
            {
                await _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest);
                return Ok("HR interviews created successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("hr/latest")]
        public async Task<IActionResult> GetLatestHrInterview()
        {
            try
            {
                List<HRInterviewDTO> hrInterviews = await _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>();
                return Ok(hrInterviews);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpPost("hr/evaluations")]
        public async Task<IActionResult> EvaluateHrInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            try
            {
                await _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);
                return Ok("HR interviews evaluated successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpPost("technical")]
        public async Task<IActionResult> GenerateTechnicalInterviews([FromBody] TechnicalRequestDTO technicalRequest)
        {
            try
            {
                await _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest);
                return Ok("Technical interviews created successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("technical/latest")]
        public async Task<IActionResult> GetLatestTechnicalInterview()
        {
            try
            {
                List<TechnicalInterviewDTO> technicalInterviews = await _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>();
                return Ok(technicalInterviews);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpPost("technical/evaluations")]
        public async Task<IActionResult> EvaluateTechnicalInterviews([FromBody] List<EvaluateRequestDTO> evaluateRequests)
        {
            try
            {
                await _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests);
                return Ok("Technical interviews evaluated successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
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
    }
}