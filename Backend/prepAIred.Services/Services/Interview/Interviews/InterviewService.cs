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

        public async Task UpdateInterviewAsync<TInterview>(List<TInterview> interviews) where TInterview : Interview
        {
            _dataContext.Interviews.UpdateRange(interviews);
            await _dataContext.SaveChangesAsync();
        }

        public List<TInterviewDTO> GetLatestInterviews<TInterview, TInterviewDTO>(List<TInterview> interviews)
            where TInterview : Interview
            where TInterviewDTO : InterviewDTO
        {
            return interviews
                .Select(i => i.ToDto<TInterviewDTO>())
                .ToList();
        }

        public void UpdateExistingInterviewWithEvaluation<TInterview>(List<TInterview> evaluatedInterviews, List<TInterview> existingInterviews) where TInterview : Interview
        {
            foreach (TInterview evaluatedInterview in evaluatedInterviews)
            {
                TInterview? existing = existingInterviews.FirstOrDefault(i => i.ID == evaluatedInterview.ID);
                if (existing is null) return;

                existing.Score = evaluatedInterview.Score;
                existing.Feedback = evaluatedInterview.Feedback;
                existing.SelectedAnswer = evaluatedInterview.SelectedAnswer;
                existing.IsAnswered = evaluatedInterview.IsAnswered;
                existing.Answers = evaluatedInterview.Answers;
            }
        }
    }
}
