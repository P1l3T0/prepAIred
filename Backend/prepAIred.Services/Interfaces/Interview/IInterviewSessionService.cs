using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Defines methods for managing interview session entities.
    /// </summary>
    public interface IInterviewSessionService
    {
        /// <summary>
        /// Creates and persists a new interview session entity asynchronously.
        /// </summary>
        /// <param name="interviewSession">The <see cref="InterviewSession"/> entity to create and store.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateInterviewSessionAsync(InterviewSession interviewSession);

        /// <summary>
        /// Retrieves all interviews associated with a specific user by their unique identifier.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="InterviewSessionDTO"/> objects.</returns>
        Task<List<InterviewSessionDTO>> GetInterviewSessionsByUserIdAsync(int userID);
    }
}
