using prepAIred.Data;

namespace prepAIred.Services
{
    public interface IInterviewSessionService
    {
        Task CreateInterviewSessionAsync(InterviewSession interviewSession);
    }
}
