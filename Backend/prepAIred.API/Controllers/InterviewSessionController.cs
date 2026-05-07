using prepAIred.Data;
using prepAIred.Services;
using prepAIred.Exceptions;
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
            try
            {
                List<InterviewSessionDTO> interviewSessions = await _interviewSessionService.GetInterviewSessionDTOsAsync();
                return Ok(interviewSessions);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-interview-session-activities")]
        public async Task<IActionResult> GetInterviewSessionActivities()
        {
            try
            {
                List<InterviewSessionActivityDTO> interviewSessionActivities = await _interviewSessionService.GetInterviewSessionActivitiesAsync();
                return Ok(interviewSessionActivities);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-interview-session-statistics")]
        public async Task<IActionResult> GetInterviewSessionStatistics()
        {
            try
            {
                ProfileStatisticsDTO profileStats = await _interviewSessionService.GetInterviewSessionStatisticsAsync();
                return Ok(profileStats);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-interview-session-performance")]
        public async Task<IActionResult> GetInterviewSessionPerformance()
        {
            try
            {
                List<InterviewSessionPerformanceDTO> performanceData = await _interviewSessionService.GetInterviewSessionPerformanceAsync();
                return Ok(performanceData);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-interview-session-programming-language-data")]
        public async Task<IActionResult> GetInterviewSessionProgrammingLanguageData()
        {
            try
            {
                List<ProgrammingLanguageDataDTO> programmingLanguageData = await _interviewSessionService.GetInterviewSessionProgrammingLanguageDataAsync();
                return Ok(programmingLanguageData);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-interview-session-position-data")]
        public async Task<IActionResult> GetInterviewSessionPositionData()
        {
            try
            {
                List<PositionDataDTO> positionData = await _interviewSessionService.GetInterviewSessionPositionDataAsync();
                return Ok(positionData);
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("finish-interview-session")]
        public async Task<IActionResult> FinishInterviewSession()
        {
            try
            {
                await _interviewSessionService.FinishInterviewSessionAsync();
                return Ok("Interview session finished successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-interview-sessions")]
        public async Task<IActionResult> DeleteInterviewSessions()
        {
            try
            {
                await _interviewSessionService.DeleteInterviewSessionsAsync();
                return Ok("All interview sessions deleted successfully.");
            }
            catch (InterviewSessionNotFoundException ex)
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
