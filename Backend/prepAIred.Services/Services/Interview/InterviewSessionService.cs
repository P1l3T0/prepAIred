using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewSessionService(DataContext dataContext) : IInterviewSessionService
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task CreateInterviewSessionAsync(InterviewSession interviewSession)
        {
            await _dataContext.InterviewSessions.AddRangeAsync(interviewSession);
        }
    }
}
