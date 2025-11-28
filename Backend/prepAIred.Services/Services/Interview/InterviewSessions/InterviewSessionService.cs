using Microsoft.EntityFrameworkCore;
using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewSessionService(DataContext dataContext) : IInterviewSessionService
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task CreateInterviewSessionAsync(InterviewSession interviewSession)
        {
            await _dataContext.InterviewSessions.AddAsync(interviewSession);
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
                .FirstOrDefaultAsync();
        }

        public async Task<List<InterviewSession>> GetInterviewSessionsByUserIdAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID)
                .Include(s => s.Interviews)
                .ToListAsync();
        }

        public async Task<InterviewSession> GetInterviewSessionByIdAsync(int sessionID)
        {
            return await _dataContext.InterviewSessions
                .Where(s => s.ID == sessionID)
                .FirstOrDefaultAsync();
        }

        public async Task<InterviewSession> GetInterviewSessionFromQuestionsAsync(List<EvaluateRequestDTO> evaluateRequests)
        {
            EvaluateRequestDTO firstRequest = evaluateRequests.FirstOrDefault()!;
            string firstQuestion = firstRequest?.Question!;

            return await _dataContext.InterviewSessions
                .Include(s => s.Interviews)
                .Where(s => s.Interviews.Any(i => i.Question == firstQuestion && !i.IsAnswered))
                .FirstOrDefaultAsync();
        }

        public async Task DeleteInterviewSessionsAsync(List<InterviewSession> interviewSessions)
        {
            _dataContext.InterviewSessions.RemoveRange(interviewSessions);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int> GetLatestInterviewSessionID(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID)
                .OrderByDescending(s => s.DateCreated)
                .Select(s => s.ID)
                .FirstOrDefaultAsync();
        }

        public async Task FinishInterviewSessionAsync(InterviewSession interviewSession)
        {
            if (interviewSession.AverageScore > 5)
            {
                interviewSession.Status = InterviewSessionStatus.Passed;
            }
            else
            {
                interviewSession.Status = InterviewSessionStatus.Failed;
            }

            _dataContext.InterviewSessions.Update(interviewSession);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalInterviewSessionsAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID && intSession.Status != InterviewSessionStatus.Ongoing)
                .CountAsync();
        }

        public async Task<int> GetPassedInterviewSessionsAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID && intSession.Status == InterviewSessionStatus.Passed)
                .CountAsync();
        }

        public async Task<int> GetOngoingInterviewSessionsAsync(int userID)
        {
            return await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID && intSession.Status == InterviewSessionStatus.Ongoing)
                .CountAsync();
        }

        public async Task<decimal> GetAverageScoreAsync(int userID)
        {
            List<InterviewSession> sessions = await _dataContext.InterviewSessions
                .Where(intSession => intSession.UserID == userID && intSession.Status != InterviewSessionStatus.Ongoing)
                .ToListAsync();

            decimal averageScore = (decimal)(sessions.Count != 0 ? sessions.Average(s => s.AverageScore) : 0);

            return averageScore;
        }

        public async Task<decimal> GetCompletionRateAsync(int userID)
        {
            int totalSessions = await GetTotalInterviewSessionsAsync(userID);
            int passedSessions = await GetPassedInterviewSessionsAsync(userID);

            if (totalSessions == 0) return 0;

            decimal rate = (passedSessions / (decimal)totalSessions) * 100;

            return Math.Round(rate, 2);
        }

        public void FinalizeInterviewSession<TInterview>(InterviewSession interviewSession, List<TInterview> evaluatedTInterviews) where TInterview : Interview
        {
            if (interviewSession.HrScore == 0)
            {
                float hrScore = evaluatedTInterviews.OfType<HRInterview>().Any()
                    ? evaluatedTInterviews.OfType<HRInterview>().Average(i => i.Score)
                    : 0;

                interviewSession.HrScore = hrScore;
            }

            if (interviewSession.TechnicalScore == 0)
            {
                float technicalScore = evaluatedTInterviews.OfType<TechnicalInterview>().Any()
                    ? evaluatedTInterviews.OfType<TechnicalInterview>().Average(i => i.Score)
                    : 0;

                interviewSession.TechnicalScore = technicalScore;
            }
        }

        public async Task<List<InterviewSessionActivityDTO>> GetInterviewSessionActivitiesAsync(int userID)
        {
            List<InterviewSession> interviewSessions = await _dataContext.InterviewSessions
                .Where(session => session.UserID == userID && session.Status != InterviewSessionStatus.Ongoing)
                .Include(session => session.Interviews)
                .OrderByDescending(session => session.DateCreated)
                .ToListAsync();

            List<InterviewSessionActivityDTO> activityDTOs = interviewSessions.ConvertAll(session => new InterviewSessionActivityDTO()
            {
                ID = session.ID,
                DateCreated = session.DateCreated,
                Subject = session.Subject,
                AverageScore = session.AverageScore,
                AiAgent = session.AIAgent.ToString(),
                InterviewTypes = session.Interviews.Select(i => i.InterviewType.ToString()).Distinct().ToList(),
                Status = session.Status.ToString()
            });

            return activityDTOs;
        }
    }
}
