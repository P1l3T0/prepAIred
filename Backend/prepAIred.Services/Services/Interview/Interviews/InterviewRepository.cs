using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Provides operations for generating and retrieving AI-generated interviews, including session management and user context.
    /// Coordinates between AI services, user services, and interview/session services to create and fetch interview data.
    /// </summary>
    /// <param name="aIService">The AI service used to generate interview questions and answers.</param>
    /// <param name="userService">The user service for retrieving user information and context.</param>
    /// <param name="interviewService">The interview service for managing interview entities.</param>
    /// <param name="interviewSessionService">The interview session service for managing interview session entities.</param>
    /// <param name="promptService">The prompt service for generating prompts for the AI agent.</param>
    public class InterviewRepository(IAIService aIService, IUserService userService, IInterviewService interviewService,
        IInterviewSessionService interviewSessionService, IPromptService promptService) : IInterviewRepository
    {
        private readonly IAIService _aIService = aIService;
        private readonly IUserService _userService = userService;
        private readonly IInterviewService _interviewService = interviewService;
        private readonly IInterviewSessionService _interviewSessionService = interviewSessionService;
        private readonly IPromptService _promptService = promptService;

        public async Task GenerateInterviewsAsync<TInterview>(BaseRequestDTO request) where TInterview : class
        {
            int currentUserID = await _userService.GetCurrentUserID();
            User currentUser = await _userService.GetUserEntityByIdAsync(currentUserID);
            AIAgent aiAgent = Enum.Parse<AIAgent>(request.AIAgent);

            await (typeof(TInterview).Name switch
            {
                nameof(HRInterview) when request is HrRequestDTO hrRequest => CreateHrInterviewAsync(hrRequest, currentUser, aiAgent),
                nameof(TechnicalInterview) when request is TechnicalRequestDTO techRequest => CreateTechnicalInterviewAsync(techRequest, currentUser, aiAgent),
                _ => Task.CompletedTask
            });
        }

        public async Task<List<TInterviewDTO>> GetLatestInterviews<TInterview, TInterviewDTO>()
            where TInterview : Interview
            where TInterviewDTO : InterviewDTO
        {
            int currentUserID = await _userService.GetCurrentUserID();
            int latestSessionID = await _interviewSessionService.GetLatestInterviewSessionID(currentUserID);

            List<TInterview> interviews = await _interviewService.GetInterviewsBySessionIdAsync<TInterview>(latestSessionID);
            List<TInterviewDTO> interviewDTOs = interviews.Select(i => i.ToDto<TInterviewDTO>()).ToList();

            return interviewDTOs;
        }

        private async Task CreateHrInterviewAsync(HrRequestDTO hrRequest, User currentUser, AIAgent aiAgent)
        {
            string prompt = _promptService.CreateHrPrompt(hrRequest, currentUser.ID);
            List<Interview> interviews = await _aIService.AskAiAgentAsync<HRInterview>(aiAgent, prompt);

            InterviewSession interviewSession = new InterviewSession()
            {
                UserID = currentUser.ID,
                User = currentUser,
                Interviews = interviews,
                AIAgent = aiAgent,
                Score = InterviewSessionScore.NotRated
            };

            await _interviewSessionService.CreateInterviewSessionAsync(interviewSession);
            await _interviewService.CreateInterviewsAsync(interviews, currentUser, interviewSession);
        }

        private async Task CreateTechnicalInterviewAsync(TechnicalRequestDTO techRequest, User currentUser, AIAgent aiAgent)
        {
            string prompt = _promptService.CreateTechnicalPrompt(techRequest, currentUser.ID);
            List<Interview> interviews = await _aIService.AskAiAgentAsync<TechnicalInterview>(aiAgent, prompt);
            InterviewSession interviewSession = await _interviewSessionService.GetAdjacentInterviewSessionAsync(currentUser.ID);

            interviewSession.Subject = techRequest.Subject;

            await _interviewSessionService.UpdateInterviewSessionAsync(interviewSession);
            await _interviewService.CreateInterviewsAsync(interviews, currentUser, interviewSession);
        }
    }
}