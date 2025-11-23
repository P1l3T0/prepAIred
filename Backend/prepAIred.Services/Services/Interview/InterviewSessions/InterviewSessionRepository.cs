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

        public async Task<ProfileStatisticsDTO> GetInterviewSessionStatistics()
        {
            int currentUserID = await _userService.GetCurrentUserID();

            ProfileStatisticsDTO profileStatistics = new ProfileStatisticsDTO()
            {
                TotalInterviewSessions = await _interviewSessionService.GetTotalInterviewSessionsAsync(currentUserID),
                PassedInterviewSessions = await _interviewSessionService.GetPassedInterviewSessionsAsync(currentUserID),
                OngoingInterviewSessions = await _interviewSessionService.GetOngoingInterviewSessionsAsync(currentUserID),
                AverageScore = await _interviewSessionService.GetAverageScoreAsync(currentUserID),
                CompletionRate = await _interviewSessionService.GetCompletionRateAsync(currentUserID)
            };

            return profileStatistics;
        }

        public async Task FinishInterviewSessionAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            int interviewSessionID = await _interviewSessionService.GetLatestInterviewSessionID(currentUserID);
            InterviewSession latestSession = await _interviewSessionService.GetInterviewSessionByIdAsync(interviewSessionID);

            await _interviewSessionService.FinishInterviewSessionAsync(latestSession);
        }

        public async Task DeleteInterviewSessionsAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            List<InterviewSession> interviewSessions = await _interviewSessionService.GetInterviewSessionsByUserIdAsync(currentUserID);

            await _interviewSessionService.DeleteInterviewSessionsAsync(interviewSessions);
        }
    }
}
