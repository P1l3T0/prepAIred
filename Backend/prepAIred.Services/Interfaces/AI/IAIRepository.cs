using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IAIRepository
    {
        Task CreateAiInterview(AIRequestDTO aIRequest);
        Task<List<InterviewDTO>> GetAiInterview();
    }
}
