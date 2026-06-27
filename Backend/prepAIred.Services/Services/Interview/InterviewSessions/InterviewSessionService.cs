using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewSessionService(IInterviewSessionRepository interviewSessionRepository, IUserRepository userRepository) : IInterviewSessionService
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository = interviewSessionRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<List<InterviewSessionDTO>> GetInterviewSessionDTOsAsync()
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            List<InterviewSession> interviewSessions = await _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(currentUserID);
            List<InterviewSessionDTO> interviewSessionsDTOs = interviewSessions.ConvertAll(session => session.ToDto<InterviewSessionDTO>());

            return interviewSessionsDTOs;
        }

        public async Task<List<InterviewSessionActivityDTO>> GetInterviewSessionActivitiesAsync()
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            List<InterviewSessionActivityDTO> activities = await _interviewSessionRepository.GetInterviewSessionActivitiesAsync(currentUserID);

            return activities;
        }

        public async Task<ProfileStatisticsDTO> GetInterviewSessionStatisticsAsync()
        {
            int currentUserID = await _userRepository.GetCurrentUserID();

            int totalInterviewSessions = await _interviewSessionRepository.GetTotalInterviewSessionsAsync(currentUserID);
            int passedInterviewSessions = await _interviewSessionRepository.GetPassedInterviewSessionsAsync(currentUserID);
            int ongoingInterviewSessions = await _interviewSessionRepository.GetOngoingInterviewSessionsAsync(currentUserID);
            decimal averageScore = await _interviewSessionRepository.GetAverageScoreAsync(currentUserID);
            decimal completionRate = await _interviewSessionRepository.GetCompletionRateAsync(currentUserID);

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
            int currentUserID = await _userRepository.GetCurrentUserID();
            int interviewSessionID = await _interviewSessionRepository.GetLatestInterviewSessionIDAsync(currentUserID);
            InterviewSession latestSession = await _interviewSessionRepository.GetInterviewSessionByIdAsync(interviewSessionID);

            await _interviewSessionRepository.FinishInterviewSessionAsync(latestSession);
        }

        public async Task DeleteInterviewSessionsAsync()
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            List<InterviewSession> interviewSessions = await _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(currentUserID);

            await _interviewSessionRepository.DeleteInterviewSessionsAsync(interviewSessions);
        }
    }
}
