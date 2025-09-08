using Microsoft.EntityFrameworkCore;
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

        public async Task<List<InterviewSession>> GetInterviewSessionsByUserIdAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(s => s.UserID == userID)
                .Include(s => s.Interviews)
                .ToListAsync();
        }

        public async Task DeleteInterviewSessionsAsync(List<InterviewSession> interviewSessions)
        {
            _dataContext.InterviewSessions.RemoveRange(interviewSessions);
            await _dataContext.SaveChangesAsync();
        }
    }
}
