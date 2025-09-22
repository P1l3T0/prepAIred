using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewSessionRepository(IInterviewSessionService interviewSessionService, IUserService userService) : IInterviewSessionRepository
    {
        private readonly IInterviewSessionService _interviewSessionService = interviewSessionService;
        private readonly IUserService _userService = userService;

        public async Task<List<InterviewSessionDTO>> GetInterviewSessionDTOsAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            List<InterviewSession> interviewSessions = await _interviewSessionService.GetInterviewSessionsByUserIdAsync(currentUserID);
            List<InterviewSessionDTO> interviewSessionsDTOs = interviewSessions.ConvertAll(session => session.ToDto<InterviewSessionDTO>());

            return interviewSessionsDTOs;
        }

        public async Task DeleteInterviewSessionsAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            List<InterviewSession> interviewSessions = await _interviewSessionService.GetInterviewSessionsByUserIdAsync(currentUserID);

            await _interviewSessionService.DeleteInterviewSessionsAsync(interviewSessions);
        }
    }
}
