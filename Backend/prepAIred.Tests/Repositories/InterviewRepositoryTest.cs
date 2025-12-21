using FakeItEasy;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class InterviewRepositoryTest
    {
        private readonly IAIService _aiService;
        private readonly IUserService _userService;
        private readonly IInterviewService _interviewService;
        private readonly IInterviewSessionService _interviewSessionService;
        private readonly IPromptService _promptService;
        private readonly ISerializationService _serializationService;
        private readonly InterviewRepository _interviewRepository;

        public InterviewRepositoryTest()
        {
            _aiService = A.Fake<IAIService>();
            _userService = A.Fake<IUserService>();
            _interviewService = A.Fake<IInterviewService>();
            _interviewSessionService = A.Fake<IInterviewSessionService>();
            _promptService = A.Fake<IPromptService>();
            _serializationService = A.Fake<ISerializationService>();

            _interviewRepository = new InterviewRepository(
                _aiService,
                _userService,
                _interviewService,
                _interviewSessionService,
                _promptService,
                _serializationService
            );
        }

        #region GenerateInterviewsAsync - HR Tests

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_CreatesHrInterview()
        {
            HrRequestDTO hrRequest = new HrRequestDTO
            {
                AIAgent = "ChatGPT",
                SoftSkillFocus = new List<string> { "Communication" },
                ContextScenario = new List<string> { "Team Collaboration" }
            };

            int userId = 1;
            User user = new User { ID = userId, Username = "TestUser" };
            string prompt = "HR interview prompt";
            List<Interview> interviews = new List<Interview> { new HRInterview() };

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateHrPrompt(hrRequest, userId)).Returns(prompt);
            A.CallTo(() => _aiService.AskAiAgentAsync<HRInterview>(AIAgent.ChatGPT, prompt)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.CreateInterviewSessionAsync(A<InterviewSession>._)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(interviews, user, A<InterviewSession>._)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<HRInterview>(hrRequest);

            A.CallTo(() => _promptService.CreateHrPrompt(hrRequest, userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _aiService.AskAiAgentAsync<HRInterview>(AIAgent.ChatGPT, prompt)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_CreatesInterviewSession_ForHrInterview()
        {
            HrRequestDTO hrRequest = new HrRequestDTO { AIAgent = "ChatGPT" };
            int userId = 1;
            User user = new User { ID = userId };
            List<Interview> interviews = new List<Interview>();

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateHrPrompt(hrRequest, userId)).Returns("prompt");
            A.CallTo(() => _aiService.AskAiAgentAsync<HRInterview>(A<AIAgent>._, A<string>._)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.CreateInterviewSessionAsync(A<InterviewSession>._)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(A<List<Interview>>._, A<User>._, A<InterviewSession>._)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<HRInterview>(hrRequest);

            A.CallTo(() => _interviewSessionService.CreateInterviewSessionAsync(A<InterviewSession>.That.Matches(s =>
                s.UserID == userId &&
                s.Status == InterviewSessionStatus.Ongoing &&
                s.AIAgent == AIAgent.ChatGPT
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_CallsCreateInterviews_ForHrInterview()
        {
            HrRequestDTO hrRequest = new HrRequestDTO { AIAgent = "Gemini" };
            int userId = 1;
            User user = new User { ID = userId };
            List<Interview> interviews = new List<Interview> { new HRInterview() };

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateHrPrompt(hrRequest, userId)).Returns("prompt");
            A.CallTo(() => _aiService.AskAiAgentAsync<HRInterview>(A<AIAgent>._, A<string>._)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.CreateInterviewSessionAsync(A<InterviewSession>._)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(interviews, user, A<InterviewSession>._)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<HRInterview>(hrRequest);

            A.CallTo(() => _interviewService.CreateInterviewsAsync(interviews, user, A<InterviewSession>._)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GenerateInterviewsAsync - Technical Tests

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_CreatesTechnicalInterview()
        {
            TechnicalRequestDTO techRequest = new TechnicalRequestDTO
            {
                AIAgent = "Claude",
                ProgrammingLanguage = "C#",
                Subject = new List<string> { "OOP", "SOLID" },
                DifficultyLevel = "Medium"
            };

            int userId = 1;
            User user = new User { ID = userId };
            string prompt = "Technical interview prompt";
            List<Interview> interviews = new List<Interview> { new TechnicalInterview() };
            InterviewSession existingSession = new InterviewSession { ID = 5, UserID = userId };

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateTechnicalPrompt(techRequest, userId)).Returns(prompt);
            A.CallTo(() => _aiService.AskAiAgentAsync<TechnicalInterview>(AIAgent.Claude, prompt)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.GetAdjacentInterviewSessionAsync(userId)).Returns(existingSession);
            A.CallTo(() => _interviewSessionService.UpdateInterviewSessionAsync(existingSession)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(interviews, user, existingSession)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<TechnicalInterview>(techRequest);

            A.CallTo(() => _promptService.CreateTechnicalPrompt(techRequest, userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _aiService.AskAiAgentAsync<TechnicalInterview>(AIAgent.Claude, prompt)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_UpdatesSessionSubject_ForTechnicalInterview()
        {
            TechnicalRequestDTO techRequest = new TechnicalRequestDTO
            {
                AIAgent = "ChatGPT",
                Subject = new List<string> { "Algorithms", "Data Structures" }
            };

            int userId = 1;
            User user = new User { ID = userId };
            InterviewSession existingSession = new InterviewSession { ID = 5, UserID = userId, Subject = "" };
            List<Interview> interviews = new List<Interview>();

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateTechnicalPrompt(techRequest, userId)).Returns("prompt");
            A.CallTo(() => _aiService.AskAiAgentAsync<TechnicalInterview>(A<AIAgent>._, A<string>._)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.GetAdjacentInterviewSessionAsync(userId)).Returns(existingSession);
            A.CallTo(() => _interviewSessionService.UpdateInterviewSessionAsync(existingSession)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(A<List<Interview>>._, A<User>._, A<InterviewSession>._)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<TechnicalInterview>(techRequest);

            Assert.Equal("Algorithms, Data Structures", existingSession.Subject);
            A.CallTo(() => _interviewSessionService.UpdateInterviewSessionAsync(existingSession)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_GenerateInterviewsAsync_GetsAdjacentSession_ForTechnicalInterview()
        {
            TechnicalRequestDTO techRequest = new TechnicalRequestDTO { AIAgent = "ChatGPT", Subject = new List<string> { "OOP" } };
            int userId = 1;
            User user = new User { ID = userId };
            InterviewSession existingSession = new InterviewSession();
            List<Interview> interviews = new List<Interview>();

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(user);
            A.CallTo(() => _promptService.CreateTechnicalPrompt(techRequest, userId)).Returns("prompt");
            A.CallTo(() => _aiService.AskAiAgentAsync<TechnicalInterview>(A<AIAgent>._, A<string>._)).Returns(interviews);
            A.CallTo(() => _interviewSessionService.GetAdjacentInterviewSessionAsync(userId)).Returns(existingSession);
            A.CallTo(() => _interviewSessionService.UpdateInterviewSessionAsync(existingSession)).Returns(Task.CompletedTask);
            A.CallTo(() => _interviewService.CreateInterviewsAsync(A<List<Interview>>._, A<User>._, A<InterviewSession>._)).Returns(Task.CompletedTask);

            await _interviewRepository.GenerateInterviewsAsync<TechnicalInterview>(techRequest);

            A.CallTo(() => _interviewSessionService.GetAdjacentInterviewSessionAsync(userId)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region GetLatestInterviewsAsync Tests

        [Fact]
        public async Task InterviewRepository_GetLatestInterviewsAsync_ReturnsInterviews_WhenSessionIsOngoing()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId, Status = InterviewSessionStatus.Ongoing };
            List<HRInterview> interviews = new List<HRInterview>
            {
                new HRInterview { ID = 1, Question = "Question 1" },
                new HRInterview { ID = 2, Question = "Question 2" }
            };
            List<HRInterviewDTO> interviewDTOs = new List<HRInterviewDTO>
            {
                new HRInterviewDTO { ID = 1, Question = "Question 1" },
                new HRInterviewDTO { ID = 2, Question = "Question 2" }
            };

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionService.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionService.GetInterviewSessionByIdAsync(sessionId)).Returns(session);
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<HRInterview>(sessionId)).Returns(interviews);
            A.CallTo(() => _interviewService.GetLatestInterviews<HRInterview, HRInterviewDTO>(interviews)).Returns(interviewDTOs);

            List<HRInterviewDTO> result = await _interviewRepository.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task InterviewRepository_GetLatestInterviewsAsync_ReturnsEmptyList_WhenSessionIsNull()
        {
            int userId = 1;
            int sessionId = 5;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionService.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionService.GetInterviewSessionByIdAsync(sessionId)).Returns((InterviewSession)null);

            List<HRInterviewDTO> result = await _interviewRepository.GetLatestInterviewsAsync<HRInterview, HRInterviewDTO>();

            Assert.Empty(result);
        }

        [Fact]
        public async Task InterviewRepository_GetLatestInterviewsAsync_ReturnsEmptyList_WhenSessionIsNotOngoing()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId, Status = InterviewSessionStatus.Passed };

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionService.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionService.GetInterviewSessionByIdAsync(sessionId)).Returns(session);

            List<TechnicalInterviewDTO> result = await _interviewRepository.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>();

            Assert.Empty(result);
        }

        [Fact]
        public async Task InterviewRepository_GetLatestInterviewsAsync_CallsGetLatestInterviews()
        {
            int userId = 1;
            int sessionId = 5;
            InterviewSession session = new InterviewSession { ID = sessionId, Status = InterviewSessionStatus.Ongoing };
            List<TechnicalInterview> interviews = new List<TechnicalInterview>();
            List<TechnicalInterviewDTO> interviewDTOs = new List<TechnicalInterviewDTO>();

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _interviewSessionService.GetLatestInterviewSessionIDAsync(userId)).Returns(sessionId);
            A.CallTo(() => _interviewSessionService.GetInterviewSessionByIdAsync(sessionId)).Returns(session);
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<TechnicalInterview>(sessionId)).Returns(interviews);
            A.CallTo(() => _interviewService.GetLatestInterviews<TechnicalInterview, TechnicalInterviewDTO>(interviews)).Returns(interviewDTOs);

            await _interviewRepository.GetLatestInterviewsAsync<TechnicalInterview, TechnicalInterviewDTO>();

            A.CallTo(() => _interviewService.GetLatestInterviews<TechnicalInterview, TechnicalInterviewDTO>(interviews)).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region EvaluateInterviewsAsync Tests

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_CreatesHrEvaluationPrompt()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>
            {
                new EvaluateRequestDTO { Question = "Q1", Answer = "A1" }
            };

            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.ChatGPT };
            List<HRInterview> existingInterviews = new List<HRInterview> { new HRInterview() };
            string basePrompt = "Evaluate HR prompt";
            string serialized = "serialized interviews";
            string fullPrompt = "full prompt";
            List<Interview> evaluatedInterviews = new List<Interview>();

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateHrEvaluationPrompt(evaluateRequests)).Returns(basePrompt);
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<HRInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns(serialized);
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(basePrompt, serialized)).Returns(fullPrompt);
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<HRInterview>(fullPrompt, session.AIAgent)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);

            A.CallTo(() => _promptService.CreateHrEvaluationPrompt(evaluateRequests)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_CreatesTechnicalEvaluationPrompt()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>
            {
                new EvaluateRequestDTO { Question = "Q1", Answer = "A1" }
            };

            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.Gemini };
            List<TechnicalInterview> existingInterviews = new List<TechnicalInterview> { new TechnicalInterview() };
            string basePrompt = "Evaluate Tech prompt";
            string serialized = "serialized";
            string fullPrompt = "full";
            List<Interview> evaluatedInterviews = new List<Interview>();

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateTechnicalEvaluationPrompt(evaluateRequests)).Returns(basePrompt);
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<TechnicalInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns(serialized);
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(basePrompt, serialized)).Returns(fullPrompt);
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<TechnicalInterview>(fullPrompt, session.AIAgent)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests);

            A.CallTo(() => _promptService.CreateTechnicalEvaluationPrompt(evaluateRequests)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_SerializesExistingInterviews()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();
            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.ChatGPT };
            List<HRInterview> existingInterviews = new List<HRInterview>();
            List<Interview> evaluatedInterviews = new List<Interview>();

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateHrEvaluationPrompt(evaluateRequests)).Returns("prompt");
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<HRInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns("serialized");
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(A<string>._, A<string>._)).Returns("full");
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<HRInterview>(A<string>._, A<AIAgent>._)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);

            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_CallsAIServiceToEvaluate()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();
            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.Claude };
            List<TechnicalInterview> existingInterviews = new List<TechnicalInterview>();
            string fullPrompt = "evaluation prompt";
            List<Interview> evaluatedInterviews = new List<Interview>();

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateTechnicalEvaluationPrompt(evaluateRequests)).Returns("base");
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<TechnicalInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns("serialized");
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(A<string>._, A<string>._)).Returns(fullPrompt);
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<TechnicalInterview>(fullPrompt, AIAgent.Claude)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<TechnicalInterview>(evaluateRequests);

            A.CallTo(() => _aiService.EvaluateInterviewsAsync<TechnicalInterview>(fullPrompt, AIAgent.Claude)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_UpdatesExistingInterviews()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();
            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.ChatGPT };
            List<HRInterview> existingInterviews = new List<HRInterview> { new HRInterview() };
            List<Interview> evaluatedInterviews = new List<Interview> { new HRInterview() };

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateHrEvaluationPrompt(evaluateRequests)).Returns("prompt");
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<HRInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns("serialized");
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(A<string>._, A<string>._)).Returns("full");
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<HRInterview>(A<string>._, A<AIAgent>._)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);

            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task InterviewRepository_EvaluateInterviewsAsync_FinalizesInterviewSession()
        {
            List<EvaluateRequestDTO> evaluateRequests = new List<EvaluateRequestDTO>();
            InterviewSession session = new InterviewSession { ID = 1, AIAgent = AIAgent.ChatGPT };
            List<HRInterview> existingInterviews = new List<HRInterview>();
            List<Interview> evaluatedInterviews = new List<Interview>();

            A.CallTo(() => _interviewSessionService.GetInterviewSessionFromQuestionsAsync(evaluateRequests)).Returns(session);
            A.CallTo(() => _promptService.CreateHrEvaluationPrompt(evaluateRequests)).Returns("prompt");
            A.CallTo(() => _interviewService.GetInterviewsBySessionIdAsync<HRInterview>(session.ID)).Returns(existingInterviews);
            A.CallTo(() => _serializationService.SerializeCollection(existingInterviews)).Returns("serialized");
            A.CallTo(() => _promptService.GetPromptWithSerializedInterviews(A<string>._, A<string>._)).Returns("full");
            A.CallTo(() => _aiService.EvaluateInterviewsAsync<HRInterview>(A<string>._, A<AIAgent>._)).Returns(evaluatedInterviews);
            A.CallTo(() => _interviewService.UpdateInterviewAsync(existingInterviews)).Returns(Task.CompletedTask);

            await _interviewRepository.EvaluateInterviewsAsync<HRInterview>(evaluateRequests);

            A.CallTo(() => _interviewSessionService.FinalizeInterviewSession(session, A<List<HRInterview>>._)).MustHaveHappenedOnceExactly();
        }

        #endregion
    }
}
