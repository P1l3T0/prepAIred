using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIService
    {
        Task<List<Interview>> AskChatGPTAsync(User currentUser, string prompt);
        Task<List<Interview>> AskGeminiAsync(User currentUser, string prompt);
        Task<List<Interview>> AskClaudeAsync(User currentUser, string prompt);
        string CreatePrompt(AIRequestDTO aIRequest, int currentUserID);
    }
}
