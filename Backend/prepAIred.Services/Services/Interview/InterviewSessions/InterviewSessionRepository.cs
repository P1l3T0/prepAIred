using prepAIred.Data;

namespace prepAIred.Services
{
    public class InterviewSessionRepository(IInterviewSessionService interviewSessionService, IUserService userService) : IInterviewSessionRepository
    {
        private readonly IInterviewSessionService _interviewSessionService = interviewSessionService;
        private readonly IUserService _userService = userService;

        public async Task<List<InterviewSessionDTO>> GetInterviewSessionDTOsAsync()
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            List<InterviewSessionDTO> interviewDTOs = (await _interviewSessionService.GetInterviewSessionsByUserIdAsync(currentUserID)).ConvertAll(i => i.ToDto<InterviewSessionDTO>());

            return interviewDTOs;
        }

        public async Task DeleteInterviewSessionsAsync()
        {
            int currentUserID = (await _userService.GetCurrentUserAsync()).ID;
            List<InterviewSession> interviewDTOs = await _interviewSessionService.GetInterviewSessionsByUserIdAsync(currentUserID);

            await _interviewSessionService.DeleteInterviewSessionsAsync(interviewDTOs);
        }
    }
}
