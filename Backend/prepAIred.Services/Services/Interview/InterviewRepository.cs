using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewRepository(IAIService aIService, IUserService userService,
        IInterviewService interviewService, IInterviewSessionService interviewSessionService) : IInterviewRepository
    {
        private readonly IAIService _aIService = aIService;
        private readonly IUserService _userService = userService;
        private readonly IInterviewService _interviewService = interviewService;
        private readonly IInterviewSessionService _interviewSessionService = interviewSessionService;

        public async Task GenerateAiInterviews(AIRequestDTO aIRequest)
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            User currentUser = await _userService.GetUserEntityByIdAsync(currentUserID);
            string prompt = _aIService.CreatePrompt(aIRequest, currentUserID);

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