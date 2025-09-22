using prepAIred.Data;
using Microsoft.EntityFrameworkCore;

namespace prepAIred.Services
{
    public class InterviewService(DataContext dataContext) : IInterviewService
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task CreateInterviewsAsync(List<Interview> interviews, User currentUser, InterviewSession interviewSession)
        {
            foreach (Interview interview in interviews)
            {
                interview.User = currentUser;
                interview.InterviewSession = interviewSession;
                interview.InterviewSessionID = interviewSession.ID;
            }

            await _dataContext.Interviews.AddRangeAsync(interviews);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<TInterview>> GetInterviewsBySessionIdAsync<TInterview>(int sessionID) where TInterview : Interview
        {
            return await _dataContext.Interviews
                .Where(i => i.InterviewSessionID == sessionID && i.GetType() == typeof(TInterview))
                .Cast<TInterview>()
                .ToListAsync();
        }
    }
}
