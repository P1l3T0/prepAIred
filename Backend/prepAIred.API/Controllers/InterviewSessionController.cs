using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
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
        private readonly IInterviewSessionRepository _interviewSessionRepository = interviewSessionRepository;

        [HttpGet("get-interview-sessions")]
        public async Task<IActionResult> GetInterviewSessionDTOs()
        {
            List<InterviewSessionDTO> interviewSessions = await _interviewSessionRepository.GetInterviewSessionDTOsAsync();
            return Ok(interviewSessions);
        }

        [HttpGet("get-interview-session-activities")]
        public async Task<IActionResult> GetInterviewSessionActivities()
        {
            List<InterviewSessionActivityDTO> interviewSessionActivities = await _interviewSessionRepository.GetInterviewSessionActivities();
            return Ok(interviewSessionActivities);
        }

        [HttpGet("get-interview-session-statistics")]
        public async Task<IActionResult> GetInterviewSessionStatistics()
        {
            ProfileStatisticsDTO profileStats = await _interviewSessionRepository.GetInterviewSessionStatistics();
            return Ok(profileStats);
        }

        [HttpPut("finish-interview-session")]
        public async Task<IActionResult> FinishInterviewSession()
        {
            await _interviewSessionRepository.FinishInterviewSessionAsync();
            return Ok("Interview session finished successfully.");
        }

        [HttpDelete("delete-interview-sessions")]
        public async Task<IActionResult> DeleteInterviewSessions()
        {
            await _interviewSessionRepository.DeleteInterviewSessionsAsync();
            return Ok("All interview sessions deleted successfully.");
        }
    }
}
