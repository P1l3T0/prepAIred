using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Provides operations for generating and retrieving AI-generated interviews, including session management and user context.
    /// Coordinates between AI services, user services, and interview/session services to create and fetch interview data.
    /// </summary>
    /// <param name="aIService">The AI service used to generate interview questions and answers.</param>
    /// <param name="userRepository">The user service for retrieving user information and context.</param>
    /// <param name="interviewRepository">The interview service for managing interview entities.</param>
    /// <param name="interviewSessionRepository">The interview session service for managing interview session entities.</param>
    /// <param name="promptService">The prompt service for generating prompts for the AI agent.</param>
    public class InterviewService(IAIService aIService, IUserRepository userRepository, IInterviewRepository interviewRepository,
        IInterviewSessionRepository interviewSessionRepository, IPromptService promptService, ISerializationService serializationService) : IInterviewService
    {
        private readonly IAIService _aIService = aIService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IInterviewRepository _interviewRepository = interviewRepository;
        private readonly IInterviewSessionRepository _interviewSessionRepository = interviewSessionRepository;
        private readonly IPromptService _promptService = promptService;
        private readonly ISerializationService _serializationService = serializationService;

        public async Task GenerateInterviewsAsync<TInterview>(BaseRequestDTO request) where TInterview : Interview
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            User currentUser = await _userRepository.GetCurrentUserEntityByIdAsync(currentUserID);
            AIAgent aiAgent = Enum.Parse<AIAgent>(request.AIAgent);

            await (typeof(TInterview).Name switch
            {
                nameof(HRInterview) when request is HrRequestDTO hrRequest => CreateHrInterviewAsync(hrRequest, currentUser, aiAgent),
                nameof(TechnicalInterview) when request is TechnicalRequestDTO techRequest => CreateTechnicalInterviewAsync(techRequest, currentUser, aiAgent),
                _ => Task.CompletedTask
            });
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
                Status = InterviewSessionStatus.Ongoing
            };

            await _interviewSessionRepository.CreateInterviewSessionAsync(interviewSession);
            await _interviewRepository.CreateInterviewsAsync(interviews, currentUser, interviewSession);
        }

        private async Task CreateTechnicalInterviewAsync(TechnicalRequestDTO techRequest, User currentUser, AIAgent aiAgent)
        {
            string prompt = _promptService.CreateTechnicalPrompt(techRequest, currentUser.ID);
            List<Interview> interviews = await _aIService.AskAiAgentAsync<TechnicalInterview>(aiAgent, prompt);
            InterviewSession interviewSession = await _interviewSessionRepository.GetAdjacentInterviewSessionAsync(currentUser.ID);

            interviewSession.Subject = string.Join(", ", techRequest.Subject);

            await _interviewSessionRepository.UpdateInterviewSessionAsync(interviewSession);
            await _interviewRepository.CreateInterviewsAsync(interviews, currentUser, interviewSession);
        }

        public async Task<List<TInterviewDTO>> GetLatestInterviewsAsync<TInterview, TInterviewDTO>()
            where TInterview : Interview
            where TInterviewDTO : InterviewDTO
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            int latestSessionID = await _interviewSessionRepository.GetLatestInterviewSessionIDAsync(currentUserID);
            InterviewSession interviewSession = await _interviewSessionRepository.GetInterviewSessionByIdAsync(latestSessionID);

            if (interviewSession is null || interviewSession.Status != InterviewSessionStatus.Ongoing)
            {
                // After the Technical Interviews are evaluated, the status of the session is set to Passed/Failed,
                // meaning the interviews will automatically disappear after evaluation. This check prevents this behavior
                // by returning an empty list if the user hasn't clicked the "Finish Interview Session" button.
                return [];
            }

            List<TInterview> interviews = await _interviewRepository.GetInterviewsBySessionIdAsync<TInterview>(latestSessionID);
            List<TInterviewDTO> interviewDTOs = _interviewRepository.GetLatestInterviews<TInterview, TInterviewDTO>(interviews);

            return interviewDTOs;
        }

        public async Task EvaluateInterviewsAsync<TInterview>(List<EvaluateRequestDTO> evaluateRequest) where TInterview : Interview
        {
            InterviewSession interviewSession = await _interviewSessionRepository.GetInterviewSessionFromQuestionsAsync(evaluateRequest);

            string basePrompt = typeof(TInterview).Name switch
            {
                nameof(HRInterview) => _promptService.CreateHrEvaluationPrompt(evaluateRequest),
                nameof(TechnicalInterview) => _promptService.CreateTechnicalEvaluationPrompt(evaluateRequest),
                _ => string.Empty
            };

            List<TInterview> existingInterviews = await _interviewRepository.GetInterviewsBySessionIdAsync<TInterview>(interviewSession.ID);
            string serializedInterviews = _serializationService.SerializeCollection(existingInterviews);
            string prompt = _promptService.GetPromptWithSerializedInterviews(basePrompt, serializedInterviews);

            List<Interview> evaluatedInterviews = await _aIService.EvaluateInterviewsAsync<TInterview>(prompt, interviewSession.AIAgent);
            List<TInterview> evaluatedTInterviews = [..evaluatedInterviews.Cast<TInterview>()];

            _interviewRepository.UpdateExistingInterviewWithEvaluation(evaluatedTInterviews, existingInterviews);
            _interviewSessionRepository.FinalizeInterviewSession(interviewSession, evaluatedTInterviews);

            await _interviewRepository.UpdateInterviewAsync(existingInterviews);
        }
    }
}