using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIService
    {
        Task<AIResponse> AskChatGPT(User currentUser, string prompt);
        Task<AIResponse> AskGemini(User currentUser, string prompt);
        Task<AIResponse> AskClaude(User currentUser, string prompt);
    }
}
