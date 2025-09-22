using prepAIred.Data;
using Microsoft.AspNetCore.Mvc;
using prepAIred.Services;

namespace prepAIred.API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing interview sessions, including retrieving and deleting sessions.
    /// </summary>
    /// <remarks>This controller handles HTTP requests related to interview sessions. It interacts with the 
    /// <see cref="IInterviewSessionRepository"/> to perform operations such as retrieving a list of  interview sessions
    /// and deleting all existing sessions.</remarks>
    /// <param name="interviewSessionRepository"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewSessionController(IInterviewSessionRepository interviewSessionRepository) : Controller
    {
        private readonly IInterviewSessionRepository _interviewRepository = interviewSessionRepository;

        [HttpGet("get-interview-sessions")]
        public async Task<IActionResult> GetAiInterviews()
        {
            List<InterviewSessionDTO> interviewSessions = await _interviewRepository.GetInterviewSessionDTOsAsync();
            return Ok(interviewSessions);
        }

        [HttpDelete("delete-interview-sessions")]
        public async Task<IActionResult> DeleteInterviewSessions()
        {
            await _interviewRepository.DeleteInterviewSessionsAsync();
            return Ok("All interview sessions deleted successfully.");
        }
    }
}
