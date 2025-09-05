using prepAIred.Data;
using Microsoft.EntityFrameworkCore;

namespace prepAIred.Services
{
    public class InterviewService(DataContext dataContext) : IInterviewService
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task CreateInterviewsAsync(List<Interview> interviews)
        {
            await _dataContext.Interviews.AddRangeAsync(interviews);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<InterviewDTO>> GetInterviewsByUserIdAsync(int userId)
        {
            return await _dataContext.Interviews
                .Where(i => i.UserID == userId && !i.IsAnswered)
                .Select(i => i.ToDto<InterviewDTO>())
                .ToListAsync();
        }
    }
}
