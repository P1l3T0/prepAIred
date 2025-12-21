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

        public async Task<List<InterviewSessionActivityDTO>> GetInterviewSessionActivitiesAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            List<InterviewSessionActivityDTO> activities = await _interviewSessionService.GetInterviewSessionActivitiesAsync(currentUserID);

            return activities;
        }

        public async Task<ProfileStatisticsDTO> GetInterviewSessionStatisticsAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();

            int totalInterviewSessions = await _interviewSessionService.GetTotalInterviewSessionsAsync(currentUserID);
            int passedInterviewSessions = await _interviewSessionService.GetPassedInterviewSessionsAsync(currentUserID);
            int ongoingInterviewSessions = await _interviewSessionService.GetOngoingInterviewSessionsAsync(currentUserID);
            decimal averageScore = await _interviewSessionService.GetAverageScoreAsync(currentUserID);
            decimal completionRate = await _interviewSessionService.GetCompletionRateAsync(currentUserID);

            ProfileStatisticsDTO profileStatistics = new ProfileStatisticsDTO()
            {
                TotalInterviewSessions = totalInterviewSessions,
                PassedInterviewSessions = passedInterviewSessions,
                OngoingInterviewSessions = ongoingInterviewSessions,
                AverageScore = averageScore,
                CompletionRate = completionRate
            };

            return profileStatistics;
        }

        public async Task<List<InterviewSessionPerformanceDTO>> GetInterviewSessionPerformanceAsync()
        {
            List<InterviewSessionActivityDTO> activities = await GetInterviewSessionActivitiesAsync();
            List<InterviewSessionPerformanceDTO> performanceData = activities
                .OrderBy(activity => activity.DateCreated)
                .Select(activity => new InterviewSessionPerformanceDTO()
                {
                    ID = activity.ID,
                    DateCreated = activity.DateCreated,
                    Score = activity.AverageScore
                }).ToList();

            return performanceData;
        }

        public async Task<List<ProgrammingLanguageDataDTO>> GetInterviewSessionProgrammingLanguageDataAsync()
        {
            List<InterviewSessionActivityDTO> activities = await GetInterviewSessionActivitiesAsync();
            List<ProgrammingLanguageDataDTO> programmingLanguageData = activities
                .GroupBy(activity => activity.ProgrammingLanguage)
                .Select(activity => new ProgrammingLanguageDataDTO()
                {
                    Language = activity.Key,
                    Sessions = activity.Count()
                }).ToList();

            return programmingLanguageData;
        }

        public async Task<List<PositionDataDTO>> GetInterviewSessionPositionDataAsync()
        {
            List<InterviewSessionActivityDTO> activities = await GetInterviewSessionActivitiesAsync();
            List<PositionDataDTO> positionData = activities
                .GroupBy(activity => activity.Position)
                .Select(activity => new PositionDataDTO()
                {
                    Position = activity.Key,
                    Sessions = activity.Count()
                }).ToList();

            return positionData;
        }

        public async Task FinishInterviewSessionAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            int interviewSessionID = await _interviewSessionService.GetLatestInterviewSessionIDAsync(currentUserID);
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
