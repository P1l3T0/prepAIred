using prepAIred.Data;
using prepAIred.Services;
using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    /// <summary>
    /// Provides endpoints for managing interview sessions, including retrieving and deleting sessions.
    /// </summary>
    /// <remarks>This controller handles HTTP requests related to interview sessions. It interacts with the 
    /// <see cref="IInterviewSessionService"/> to perform operations such as retrieving a list of  interview sessions
    /// and deleting all existing sessions.</remarks>
    /// <param name="interviewSessionService"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewSessionController(IInterviewSessionService interviewSessionService) : Controller
    {
        private readonly IInterviewSessionService _interviewSessionService = interviewSessionService;

        [HttpGet("get-interview-sessions")]
        public async Task<IActionResult> GetInterviewSessionDTOs()
        {
            List<InterviewSessionDTO> interviewSessions = await _interviewSessionService.GetInterviewSessionDTOsAsync();
            return Ok(interviewSessions);
        }

        [HttpGet("get-interview-session-activities")]
        public async Task<IActionResult> GetInterviewSessionActivities()
        {
            List<InterviewSessionActivityDTO> interviewSessionActivities = await _interviewSessionService.GetInterviewSessionActivitiesAsync();
            return Ok(interviewSessionActivities);
        }

        [HttpGet("get-interview-session-statistics")]
        public async Task<IActionResult> GetInterviewSessionStatistics()
        {
            ProfileStatisticsDTO profileStats = await _interviewSessionService.GetInterviewSessionStatisticsAsync();
            return Ok(profileStats);
        }

        [HttpGet("get-interview-session-performance")]
        public async Task<IActionResult> GetInterviewSessionPerformance()
        {
            List<InterviewSessionPerformanceDTO> performanceData = await _interviewSessionService.GetInterviewSessionPerformanceAsync();
            return Ok(performanceData);
        }

        [HttpGet("get-interview-session-programming-language-data")]
        public async Task<IActionResult> GetInterviewSessionProgrammingLanguageData()
        {
            List<ProgrammingLanguageDataDTO> programmingLanguageData = await _interviewSessionService.GetInterviewSessionProgrammingLanguageDataAsync();
            return Ok(programmingLanguageData);
        }

        [HttpGet("get-interview-session-position-data")]
        public async Task<IActionResult> GetInterviewSessionPositionData()
        {
            List<PositionDataDTO> positionData = await _interviewSessionService.GetInterviewSessionPositionDataAsync();
            return Ok(positionData);
        }

        [HttpPut("finish-interview-session")]
        public async Task<IActionResult> FinishInterviewSession()
        {
            await _interviewSessionService.FinishInterviewSessionAsync();
            return Ok("Interview session finished successfully.");
        }

        [HttpDelete("delete-interview-sessions")]
        public async Task<IActionResult> DeleteInterviewSessions()
        {
            await _interviewSessionService.DeleteInterviewSessionsAsync();
            return Ok("All interview sessions deleted successfully.");
        }
    }
}
