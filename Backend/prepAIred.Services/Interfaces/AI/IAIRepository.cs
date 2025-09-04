using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIRepository
    {
        Task<List<InterviewDTO>> GetAiInterview(AIRequestDTO aIRequest);
    }
}
