using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewRepository(IAIService aIService, IUserService userService, IInterviewService interviewService) : IInterviewRepository
    {
        private readonly IAIService _aIService = aIService;
        private readonly IUserService _userService = userService;
        private readonly IInterviewService _interviewService = interviewService;

        public async Task GenerateAiInterviews(AIRequestDTO aIRequest)
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            string prompt = _aIService.CreatePrompt(aIRequest, currentUserID);

            AIAgent aiAgent = Enum.Parse<AIAgent>(aIRequest.AIAgent);
            List<Interview> interviews = await _aIService.AskAiAgentAsync(aiAgent, prompt);

            await _interviewService.CreateInterviewsAsync(interviews);
        }

        public async Task<List<InterviewDTO>> GetAiInterview()
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            List<InterviewDTO> interviewDTOs = await _interviewService.GetInterviewsByUserIdAsync(currentUserID);

            return interviewDTOs;
        }
    }
}