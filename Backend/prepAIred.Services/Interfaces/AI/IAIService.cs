using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIService
    {
        Task<AIInterviewSession> AskChatGPT(User currentUser, string prompt);
        Task<AIInterviewSession> AskGemini(User currentUser, string prompt);
        Task<AIInterviewSession> AskClaude(User currentUser, string prompt);
        string CreatePrompt(AIRequestDTO aIRequest);
    }
}
