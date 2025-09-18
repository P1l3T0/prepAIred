using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Defines methods for managing interview session data.
    /// </summary>
    public interface IInterviewSessionRepository
    {
        /// <summary>
        /// Asynchronously retrieves a list of interview session data transfer objects (DTOs).
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of  
        /// <see cref="InterviewSessionDTO"/> objects representing the interview sessions.</returns>
        Task<List<InterviewSessionDTO>> GetInterviewSessionDTOsAsync();

        /// <summary>
        /// Deletes all interview sessions asynchronously.
        /// </summary>
        Task DeleteInterviewSessionsAsync();
    }
}
