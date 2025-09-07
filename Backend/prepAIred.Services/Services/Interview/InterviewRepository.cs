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

        public async Task GenerateAiInterviews(AIRequestDTO aIRequest)
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            User currentUser = await _userService.GetUserEntityByIdAsync(currentUserID);
            string prompt = _promptService.CreatePrompt(aIRequest, currentUserID);

            AIAgent aiAgent = Enum.Parse<AIAgent>(aIRequest.AIAgent);
            List<Interview> interviews = await _aIService.AskAiAgentAsync(aiAgent, prompt);

            InterviewSession interviewSession = new InterviewSession()
            {
                UserID = currentUserID,
                User = currentUser,
                Interviews = interviews,
                Topic = aIRequest.Topic,
                AIAgent = aiAgent,
                Score = InterviewSessionScore.NotRated
            };

            await _interviewSessionService.CreateInterviewSessionAsync(interviewSession);
            await _interviewService.CreateInterviewsAsync(interviews, currentUser, interviewSession);
        }

        public async Task<List<InterviewDTO>> GetAiInterview()
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            List<InterviewDTO> interviewDTOs = await _interviewService.GetInterviewsByUserIdAsync(currentUserID);

            return interviewDTOs;
        }
    }
}