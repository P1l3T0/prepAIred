using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIRepository
    {
        Task<AIInterviewSession> GetAIInterviewSession(AIRequestDTO aIRequest);
    }
}
