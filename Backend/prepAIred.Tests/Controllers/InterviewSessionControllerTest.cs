using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using prepAIred.API;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Controllers
{
    public class InterviewSessionControllerTest
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository;
        private readonly InterviewSessionController _interviewSessionController;

        public InterviewSessionControllerTest()
        {
            _interviewSessionRepository = A.Fake<IInterviewSessionRepository>();
            _interviewSessionController = new InterviewSessionController(_interviewSessionRepository);
        }

        #region GetInterviewSessionDTOs Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionDTOs_ReturnsOk()
        {
            List<InterviewSessionDTO> interviewSessions = new List<InterviewSessionDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionDTOsAsync()).Returns(interviewSessions);

            IActionResult result = await _interviewSessionController.GetInterviewSessionDTOs();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionDTOs_ReturnsListOfSessions()
        {
            List<InterviewSessionDTO> interviewSessions = new List<InterviewSessionDTO>
            {
                new InterviewSessionDTO { ID = 1, Subject = "C# Basics" },
                new InterviewSessionDTO { ID = 2, Subject = "Python Advanced" }
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionDTOsAsync()).Returns(interviewSessions);

            IActionResult result = await _interviewSessionController.GetInterviewSessionDTOs();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<InterviewSessionDTO> returnedSessions = Assert.IsType<List<InterviewSessionDTO>>(okResult.Value);
            Assert.Equal(2, returnedSessions.Count);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionDTOs_CallsRepositoryMethod()
        {
            List<InterviewSessionDTO> interviewSessions = new List<InterviewSessionDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionDTOsAsync()).Returns(interviewSessions);

            await _interviewSessionController.GetInterviewSessionDTOs();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionDTOsAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionActivities Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionActivities_ReturnsOk()
        {
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync()).Returns(activities);

            IActionResult result = await _interviewSessionController.GetInterviewSessionActivities();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionActivities_ReturnsListOfActivities()
        {
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>
            {
                new InterviewSessionActivityDTO { ID = 1, Subject = "C# OOP", AverageScore = 8.5f },
                new InterviewSessionActivityDTO { ID = 2, Subject = "Python ML", AverageScore = 7.2f }
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync()).Returns(activities);

            IActionResult result = await _interviewSessionController.GetInterviewSessionActivities();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<InterviewSessionActivityDTO> returnedActivities = Assert.IsType<List<InterviewSessionActivityDTO>>(okResult.Value);
            Assert.Equal(2, returnedActivities.Count);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionActivities_CallsRepositoryMethod()
        {
            List<InterviewSessionActivityDTO> activities = new List<InterviewSessionActivityDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync()).Returns(activities);

            await _interviewSessionController.GetInterviewSessionActivities();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionActivitiesAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionStatistics Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionStatistics_ReturnsOk()
        {
            ProfileStatisticsDTO stats = new ProfileStatisticsDTO();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionStatisticsAsync()).Returns(stats);

            IActionResult result = await _interviewSessionController.GetInterviewSessionStatistics();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionStatistics_ReturnsStatistics()
        {
            ProfileStatisticsDTO stats = new ProfileStatisticsDTO
            {
                TotalInterviewSessions = 10,
                PassedInterviewSessions = 7,
                AverageScore = 8.5m
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionStatisticsAsync()).Returns(stats);

            IActionResult result = await _interviewSessionController.GetInterviewSessionStatistics();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            ProfileStatisticsDTO returnedStats = Assert.IsType<ProfileStatisticsDTO>(okResult.Value);
            Assert.Equal(10, returnedStats.TotalInterviewSessions);
            Assert.Equal(7, returnedStats.PassedInterviewSessions);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionStatistics_CallsRepositoryMethod()
        {
            ProfileStatisticsDTO stats = new ProfileStatisticsDTO();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionStatisticsAsync()).Returns(stats);

            await _interviewSessionController.GetInterviewSessionStatistics();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionStatisticsAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionPerformance Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPerformance_ReturnsOk()
        {
            List<InterviewSessionPerformanceDTO> performance = new List<InterviewSessionPerformanceDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPerformanceAsync()).Returns(performance);

            IActionResult result = await _interviewSessionController.GetInterviewSessionPerformance();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPerformance_ReturnsPerformanceData()
        {
            List<InterviewSessionPerformanceDTO> performance = new List<InterviewSessionPerformanceDTO>
            {
                new InterviewSessionPerformanceDTO { ID = 1, Score = 8.0f },
                new InterviewSessionPerformanceDTO { ID = 2, Score = 9.0f }
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPerformanceAsync()).Returns(performance);

            IActionResult result = await _interviewSessionController.GetInterviewSessionPerformance();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<InterviewSessionPerformanceDTO> returnedPerformance = Assert.IsType<List<InterviewSessionPerformanceDTO>>(okResult.Value);
            Assert.Equal(2, returnedPerformance.Count);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPerformance_CallsRepositoryMethod()
        {
            List<InterviewSessionPerformanceDTO> performance = new List<InterviewSessionPerformanceDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPerformanceAsync()).Returns(performance);

            await _interviewSessionController.GetInterviewSessionPerformance();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPerformanceAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionProgrammingLanguageData Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionProgrammingLanguageData_ReturnsOk()
        {
            List<ProgrammingLanguageDataDTO> languageData = new List<ProgrammingLanguageDataDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionProgrammingLanguageDataAsync()).Returns(languageData);

            IActionResult result = await _interviewSessionController.GetInterviewSessionProgrammingLanguageData();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionProgrammingLanguageData_ReturnsLanguageData()
        {
            List<ProgrammingLanguageDataDTO> languageData = new List<ProgrammingLanguageDataDTO>
            {
                new ProgrammingLanguageDataDTO { Language = "C#", Sessions = 5 },
                new ProgrammingLanguageDataDTO { Language = "Python", Sessions = 3 }
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionProgrammingLanguageDataAsync()).Returns(languageData);

            IActionResult result = await _interviewSessionController.GetInterviewSessionProgrammingLanguageData();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<ProgrammingLanguageDataDTO> returnedData = Assert.IsType<List<ProgrammingLanguageDataDTO>>(okResult.Value);
            Assert.Equal(2, returnedData.Count);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionProgrammingLanguageData_CallsRepositoryMethod()
        {
            List<ProgrammingLanguageDataDTO> languageData = new List<ProgrammingLanguageDataDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionProgrammingLanguageDataAsync()).Returns(languageData);

            await _interviewSessionController.GetInterviewSessionProgrammingLanguageData();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionProgrammingLanguageDataAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetInterviewSessionPositionData Tests

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPositionData_ReturnsOk()
        {
            List<PositionDataDTO> positionData = new List<PositionDataDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPositionDataAsync()).Returns(positionData);

            IActionResult result = await _interviewSessionController.GetInterviewSessionPositionData();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPositionData_ReturnsPositionData()
        {
            List<PositionDataDTO> positionData = new List<PositionDataDTO>
            {
                new PositionDataDTO { Position = "Junior Developer", Sessions = 4 },
                new PositionDataDTO { Position = "Senior Developer", Sessions = 6 }
            };

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPositionDataAsync()).Returns(positionData);

            IActionResult result = await _interviewSessionController.GetInterviewSessionPositionData();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<PositionDataDTO> returnedData = Assert.IsType<List<PositionDataDTO>>(okResult.Value);
            Assert.Equal(2, returnedData.Count);
        }

        [Fact]
        public async Task InterviewSessionController_GetInterviewSessionPositionData_CallsRepositoryMethod()
        {
            List<PositionDataDTO> positionData = new List<PositionDataDTO>();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPositionDataAsync()).Returns(positionData);

            await _interviewSessionController.GetInterviewSessionPositionData();

            A.CallTo(() => _interviewSessionRepository.GetInterviewSessionPositionDataAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region FinishInterviewSession Tests

        [Fact]
        public async Task InterviewSessionController_FinishInterviewSession_ReturnsOk()
        {
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _interviewSessionController.FinishInterviewSession();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_FinishInterviewSession_ReturnsSuccessMessage()
        {
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _interviewSessionController.FinishInterviewSession();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Interview session finished successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewSessionController_FinishInterviewSession_CallsRepositoryMethod()
        {
            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync()).Returns(Task.CompletedTask);

            await _interviewSessionController.FinishInterviewSession();

            A.CallTo(() => _interviewSessionRepository.FinishInterviewSessionAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region DeleteInterviewSessions Tests

        [Fact]
        public async Task InterviewSessionController_DeleteInterviewSessions_ReturnsOk()
        {
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _interviewSessionController.DeleteInterviewSessions();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewSessionController_DeleteInterviewSessions_ReturnsSuccessMessage()
        {
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _interviewSessionController.DeleteInterviewSessions();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("All interview sessions deleted successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewSessionController_DeleteInterviewSessions_CallsRepositoryMethod()
        {
            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync()).Returns(Task.CompletedTask);

            await _interviewSessionController.DeleteInterviewSessions();

            A.CallTo(() => _interviewSessionRepository.DeleteInterviewSessionsAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion
    }
}
