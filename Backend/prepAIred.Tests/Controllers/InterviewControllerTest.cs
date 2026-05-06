using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using prepAIred.API;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Controllers
{
    public class InterviewControllerTest
    {
        private readonly IInterviewService _interviewService;
        private readonly InterviewController _interviewController;

        public InterviewControllerTest()
        {
            _interviewService = A.Fake<IInterviewService>();
            _interviewController = new InterviewController(_interviewService);
        }

        #region GenerateHrInterview Tests

        [Fact]
        public async Task InterviewController_GenerateHrInterview_ReturnsOk()
        {
            HrRequestDTO hrRequest = new HrRequestDTO
            {
                AIAgent = "ChatGPT",
                SoftSkillFocus = new List<string> { "Communication" },
                ContextScenario = new List<string> { "Team Collaboration" }
            };

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.GenerateHrInterview(hrRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_GenerateHrInterview_ReturnsSuccessMessage()
        {
            HrRequestDTO hrRequest = new HrRequestDTO();

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.GenerateHrInterview(hrRequest);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("HR interviews created successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewController_GenerateHrInterview_CallsRepositoryWithCorrectType()
        {
            HrRequestDTO hrRequest = new HrRequestDTO();

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest)).Returns(Task.CompletedTask);

            await _interviewController.GenerateHrInterview(hrRequest);

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<HRInterview>(hrRequest)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetLatestHrInterview Tests

        [Fact]
        public async Task InterviewController_GetLatestHrInterview_ReturnsOk()
        {
            List<HRInterviewDTO> interviews = new List<HRInterviewDTO>();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>()).Returns(interviews);

            IActionResult result = await _interviewController.GetLatestHrInterview();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_GetLatestHrInterview_ReturnsInterviewList()
        {
            List<HRInterviewDTO> interviews = new List<HRInterviewDTO>
            {
                new HRInterviewDTO { ID = 1, Question = "Tell me about a time...", SoftSkillFocus = "Communication" },
                new HRInterviewDTO { ID = 2, Question = "How do you handle...", SoftSkillFocus = "Teamwork" }
            };

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>()).Returns(interviews);

            IActionResult result = await _interviewController.GetLatestHrInterview();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<HRInterviewDTO> returnedInterviews = Assert.IsType<List<HRInterviewDTO>>(okResult.Value);
            Assert.Equal(2, returnedInterviews.Count);
        }

        [Fact]
        public async Task InterviewController_GetLatestHrInterview_CallsRepositoryMethod()
        {
            List<HRInterviewDTO> interviews = new List<HRInterviewDTO>();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>()).Returns(interviews);

            await _interviewController.GetLatestHrInterview();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region EvaluateHrInterviews Tests

        [Fact]
        public async Task InterviewController_EvaluateHrInterviews_ReturnsOk()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>
            {
                new EvaluateRequestDTO { Question = "Question 1", Answer = "Answer 1" }
            };

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.EvaluateHrInterviews(evaluateRequests);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_EvaluateHrInterviews_ReturnsSuccessMessage()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.EvaluateHrInterviews(evaluateRequests);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("HR interviews evaluated successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewController_EvaluateHrInterviews_CallsRepositoryWithCorrectType()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            await _interviewController.EvaluateHrInterviews(evaluateRequests);

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<HRInterview>(evaluateRequests)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GenerateTechnicalInterviews Tests

        [Fact]
        public async Task InterviewController_GenerateTechnicalInterviews_ReturnsOk()
        {
            TechnicalRequestDTO technicalRequest = new TechnicalRequestDTO
            {
                AIAgent = "ChatGPT",
                ProgrammingLanguage = "C#",
                Subject = new List<string> { "OOP" },
                DifficultyLevel = "Medium",
                Position = "Junior Developer"
            };

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.GenerateTechnicalInterviews(technicalRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_GenerateTechnicalInterviews_ReturnsSuccessMessage()
        {
            TechnicalRequestDTO technicalRequest = new TechnicalRequestDTO();

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.GenerateTechnicalInterviews(technicalRequest);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Technical interviews created successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewController_GenerateTechnicalInterviews_CallsRepositoryWithCorrectType()
        {
            TechnicalRequestDTO technicalRequest = new TechnicalRequestDTO();

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest)).Returns(Task.CompletedTask);

            await _interviewController.GenerateTechnicalInterviews(technicalRequest);

            A.CallTo(() => _interviewService.GenerateInterviewsAsync<TechnicalInterview>(technicalRequest)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetLatestTechnicalInterview Tests

        [Fact]
        public async Task InterviewController_GetLatestTechnicalInterview_ReturnsOk()
        {
            List<TechnicalInterviewDTO> interviews = new List<TechnicalInterviewDTO>();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>()).Returns(interviews);

            IActionResult result = await _interviewController.GetLatestTechnicalInterview();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_GetLatestTechnicalInterview_ReturnsInterviewList()
        {
            List<TechnicalInterviewDTO> interviews = new List<TechnicalInterviewDTO>
            {
                new TechnicalInterviewDTO { ID = 1, Question = "What is polymorphism?", ProgrammingLanguage = "C#" },
                new TechnicalInterviewDTO { ID = 2, Question = "Explain SOLID principles", ProgrammingLanguage = "C#" }
            };

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>()).Returns(interviews);

            IActionResult result = await _interviewController.GetLatestTechnicalInterview();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<TechnicalInterviewDTO> returnedInterviews = Assert.IsType<List<TechnicalInterviewDTO>>(okResult.Value);
            Assert.Equal(2, returnedInterviews.Count);
        }

        [Fact]
        public async Task InterviewController_GetLatestTechnicalInterview_CallsRepositoryMethod()
        {
            List<TechnicalInterviewDTO> interviews = new List<TechnicalInterviewDTO>();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>()).Returns(interviews);

            await _interviewController.GetLatestTechnicalInterview();

            A.CallTo(() => _interviewService.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region EvaluateTechnicalInterviews Tests

        [Fact]
        public async Task InterviewController_EvaluateTechnicalInterviews_ReturnsOk()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>
            {
                new EvaluateRequestDTO { Question = "What is OOP?", Answer = "Object-Oriented Programming" }
            };

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.EvaluateTechnicalInterviews(evaluateRequests);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InterviewController_EvaluateTechnicalInterviews_ReturnsSuccessMessage()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            IActionResult result = await _interviewController.EvaluateTechnicalInterviews(evaluateRequests);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Technical interviews evaluated successfully.", okResult.Value);
        }

        [Fact]
        public async Task InterviewController_EvaluateTechnicalInterviews_CallsRepositoryWithCorrectType()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests)).Returns(Task.CompletedTask);

            await _interviewController.EvaluateTechnicalInterviews(evaluateRequests);

            A.CallTo(() => _interviewService.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests)).MustHaveHappenedOnceExactly();
        }

        #endregion
    }
}
