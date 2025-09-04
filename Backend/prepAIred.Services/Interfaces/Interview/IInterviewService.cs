using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IInterviewService
    {
        Task CreateInterviewsAsync(List<Interview> interviews);
    }
}