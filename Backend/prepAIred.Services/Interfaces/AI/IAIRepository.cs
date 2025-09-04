using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIRepository
    {
        Task<AIResponse> GetAIResponse(AIRequestDTO aIRequest);
    }
}
