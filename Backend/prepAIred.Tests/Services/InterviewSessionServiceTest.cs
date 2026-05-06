using FakeItEasy;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class InterviewSessionServiceTest
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly InterviewSessionService _interviewSessionService;

        public InterviewSessionServiceTest()
        {
            _interviewSessionRepository = A.Fake<IInterviewSessionRepository>();
            _userRepository = A.Fake<IUserRepository>();

            _interviewSessionService = new InterviewSessionService(_interviewSessionRepository, _userRepository);
        }

        #region GetInterviewSessionDTOsAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionDTOsAsync_ReturnsConvertedDTOs()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>
            {
                new InterviewSession { ID = 1, Subject = "C# Basics", UserID = userId },
                new InterviewSession { ID = 2, Subject = "Python Advanced", UserID = userId }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);

            List<InterviewSessionDTO> result = await _interviewSessionService.GetInterviewSessionDTOsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionDTOsAsync_CallsGetCurrentUserID()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>();

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);

            await _interviewSessionService.GetInterviewSessionDTOsAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionDTOsAsync_CallsGetInterviewSessionsByUserIdAsync()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>();

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);

            await _interviewSessionService.GetInterviewSessionDTOsAsync();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionActivitiesAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionActivitiesAsync_ReturnsActivities()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ID = 1, Subject = "OOP", AverageScore = 8.5f },
                new InterviewSessionActivityDTO { ID = 2, Subject = "Algorithms", AverageScore = 7.0f }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<InterviewSessionActivityDTO> result = await _interviewSessionService.GetInterviewSessionActivitiesAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("OOP", result[0].Subject);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionActivitiesAsync_CallsServiceMethod()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>();

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            await _interviewSessionService.GetInterviewSessionActivitiesAsync();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionStatisticsAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionStatisticsAsync_ReturnsStatistics()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetTotalInterviewSessionsAsync(userId)).Returns(10);
            A.CallTo(() => _interviewSessionRepository.GetPassedInterviewSessionsAsync(userId)).Returns(7);
            A.CallTo(() => _interviewSessionRepository.GetOngoingInterviewSessionsAsync(userId)).Returns(2);
            A.CallTo(() => _interviewSessionRepository.GetAverageScoreAsync(userId)).Returns(8.5m);
            A.CallTo(() => _interviewSessionRepository.GetCompletionRateAsync(userId)).Returns(70.0m);

            ProfileStatisticsDTO result = await _interviewSessionService.GetInterviewSessionStatisticsAsync();

            Assert.Equal(10, result.TotalInterviewSessions);
            Assert.Equal(7, result.PassedInterviewSessions);
            Assert.Equal(2, result.OngoingInterviewSessions);
            Assert.Equal(8.5m, result.AverageScore);
            Assert.Equal(70.0m, result.CompletionRate);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionStatisticsAsync_CallsAllStatisticMethods()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetTotalInterviewSessionsAsync(userId)).Returns(10);
            A.CallTo(() => _interviewSessionRepository.GetPassedInterviewSessionsAsync(userId)).Returns(7);
            A.CallTo(() => _interviewSessionRepository.GetOngoingInterviewSessionsAsync(userId)).Returns(2);
            A.CallTo(() => _interviewSessionRepository.GetAverageScoreAsync(userId)).Returns(8.5m);
            A.CallTo(() => _interviewSessionRepository.GetCompletionRateAsync(userId)).Returns(70.0m);

            await _interviewSessionService.GetInterviewSessionStatisticsAsync();

            A.CallTo(() => _interviewSessionRepository.GetTotalInterviewSessionsAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _interviewSessionRepository.GetPassedInterviewSessionsAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _interviewSessionRepository.GetOngoingInterviewSessionsAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _interviewSessionRepository.GetAverageScoreAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _interviewSessionRepository.GetCompletionRateAsync(userId)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionPerformanceAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionPerformanceAsync_ReturnsOrderedPerformanceData()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ID = 2, DateCreated = new DateTime(2024, 1, 15), AverageScore = 9.0f },
                new InterviewSessionActivityDTO { ID = 1, DateCreated = new DateTime(2024, 1, 10), AverageScore = 8.0f }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<InterviewSessionPerformanceDTO> result = await _interviewSessionService.GetInterviewSessionPerformanceAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].ID);
            Assert.Equal(8.0f, result[0].Score);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionPerformanceAsync_OrdersByDateCreated()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ID = 3, DateCreated = new DateTime(2024, 1, 20), AverageScore = 7.5f },
                new InterviewSessionActivityDTO { ID = 1, DateCreated = new DateTime(2024, 1, 10), AverageScore = 8.0f },
                new InterviewSessionActivityDTO { ID = 2, DateCreated = new DateTime(2024, 1, 15), AverageScore = 9.0f }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<InterviewSessionPerformanceDTO> result = await _interviewSessionService.GetInterviewSessionPerformanceAsync();

            Assert.Equal(1, result[0].ID);
            Assert.Equal(2, result[1].ID);
            Assert.Equal(3, result[2].ID);
        }

        #endregion

        #region GetInterviewSessionProgrammingLanguageDataAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionProgrammingLanguageDataAsync_GroupsByLanguage()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ProgrammingLanguage = "C#" },
                new InterviewSessionActivityDTO { ProgrammingLanguage = "C#" },
                new InterviewSessionActivityDTO { ProgrammingLanguage = "Python" }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<ProgrammingLanguageDataDTO> result = await _interviewSessionService.GetInterviewSessionProgrammingLanguageDataAsync();

            Assert.Equal(2, result.Count);
            ProgrammingLanguageDataDTO csharpData = result.FirstOrDefault(x => x.Language == "C#");
            Assert.NotNull(csharpData);
            Assert.Equal(2, csharpData.Sessions);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionProgrammingLanguageDataAsync_CountsCorrectly()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ProgrammingLanguage = "Java" },
                new InterviewSessionActivityDTO { ProgrammingLanguage = "Python" },
                new InterviewSessionActivityDTO { ProgrammingLanguage = "Python" },
                new InterviewSessionActivityDTO { ProgrammingLanguage = "Python" }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<ProgrammingLanguageDataDTO> result = await _interviewSessionService.GetInterviewSessionProgrammingLanguageDataAsync();

            ProgrammingLanguageDataDTO pythonData = result.FirstOrDefault(x => x.Language == "Python");
            Assert.NotNull(pythonData);
            Assert.Equal(3, pythonData.Sessions);
        }

        #endregion

        #region GetInterviewSessionPositionDataAsync Tests

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionPositionDataAsync_GroupsByPosition()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { Position = "Junior Developer" },
                new InterviewSessionActivityDTO { Position = "Junior Developer" },
                new InterviewSessionActivityDTO { Position = "Senior Developer" }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<PositionDataDTO> result = await _interviewSessionService.GetInterviewSessionPositionDataAsync();

            Assert.Equal(2, result.Count);
            PositionDataDTO juniorData = result.FirstOrDefault(x => x.Position == "Junior Developer");
            Assert.NotNull(juniorData);
            Assert.Equal(2, juniorData.Sessions);
        }

        [Fact]
        public async Task InterviewSessionService_GetInterviewSessionPositionDataAsync_CountsCorrectly()
        {
            int userId = 1;
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { Position = "Lead Developer" },
                new InterviewSessionActivityDTO { Position = "Mid Developer" },
                new InterviewSessionActivityDTO { Position = "Mid Developer" },
                new InterviewSessionActivityDTO { Position = "Mid Developer" }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync(userId)).Returns(activities);

            List<PositionDataDTO> result = await _interviewSessionService.GetInterviewSessionPositionDataAsync();

            PositionDataDTO midData = result.FirstOrDefault(x => x.Position == "Mid Developer");
            Assert.NotNull(midData);
            Assert.Equal(3, midData.Sessions);
        }

        #endregion

        #region FinishInterviewSessionAsync Tests

        [Fact]
        public async Task InterviewSessionService_FinishInterviewSessionAsync_GetsLatestSession()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionByIdAsync(sessionId)).Returns(session);
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync(session)).Returns(Task.CompletedTask);

            await _interviewSessionService.FinishInterviewSessionAsync();

            A.CallTo(() => _interviewSessionRepository.GetLatestInterviewSessionIDAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewSessionService_FinishInterviewSessionAsync_CallsFinishOnService()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionByIdAsync(sessionId)).Returns(session);
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync(session)).Returns(Task.CompletedTask);

            await _interviewSessionService.FinishInterviewSessionAsync();

            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync(session)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewSessionService_FinishInterviewSessionAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionByIdAsync(sessionId)).Returns(session);
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync(session)).Returns(Task.CompletedTask);

            await _interviewSessionService.FinishInterviewSessionAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened()
                .Then(A.CallTo(() => _interviewSessionRepository.GetLatestInterviewSessionIDAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _interviewSessionRepository.GetInterviewSessionByIdAsync(sessionId)).MustHaveHappened())
                .Then(A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync(session)).MustHaveHappened());
        }

        #endregion

        #region DeleteInterviewSessionsAsync Tests

        [Fact]
        public async Task InterviewSessionService_DeleteInterviewSessionsAsync_GetsUserSessions()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>
            {
                new InterviewSession { ID = 1 },
                new InterviewSession { ID = 2 }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync(sessions)).Returns(Task.CompletedTask);

            await _interviewSessionService.DeleteInterviewSessionsAsync();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewSessionService_DeleteInterviewSessionsAsync_CallsDeleteOnService()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>
            {
                new InterviewSession { ID = 1 },
                new InterviewSession { ID = 2 }
            };

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync(sessions)).Returns(Task.CompletedTask);

            await _interviewSessionService.DeleteInterviewSessionsAsync();

            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync(sessions)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewSessionService_DeleteInterviewSessionsAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;
            List<InterviewSession> sessions = new List<InterviewSession>();

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).Returns(sessions);
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync(sessions)).Returns(Task.CompletedTask);

            await _interviewSessionService.DeleteInterviewSessionsAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened()
                .Then(A.CallTo(() => _interviewSessionRepository.GetInterviewSessionsByUserIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync(sessions)).MustHaveHappened());
        }

        #endregion
    }
}
