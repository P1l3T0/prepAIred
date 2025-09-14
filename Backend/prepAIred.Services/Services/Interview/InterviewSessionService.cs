using prepAIred.Data;
using Microsoft.EntityFrameworkCore;

namespace prepAIred.Services
{
    public class InterviewSessionService(DataContext dataContext) : IInterviewSessionService
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task CreateInterviewSessionAsync(InterviewSession interviewSession)
        {
            await _dataContext.InterviewSessions.AddRangeAsync(interviewSession);
        }

        public async Task UpdateInterviewSessionAsync(InterviewSession interviewSession)
        {
            _dataContext.InterviewSessions.Update(interviewSession);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<InterviewSession> GetAdjacentInterviewSessionAsync(int currentUserID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == currentUserID)
                .OrderByDescending(s => s.DateCreated)
                .FirstOrDefaultAsync() ?? new InterviewSession();
        }

        public async Task<List<InterviewSession>> GetInterviewSessionsByUserIdAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID)
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
