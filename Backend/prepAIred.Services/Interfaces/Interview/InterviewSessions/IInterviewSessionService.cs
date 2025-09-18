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
        /// Updates the details of an existing interview session.
        /// </summary>
        /// <remarks>This method updates the interview session with the provided details. Ensure that the
        /// <see cref="InterviewSession"/> object contains valid and complete information before calling this
        /// method.</remarks>
        /// <param name="interviewSession">The <see cref="InterviewSession"/> object containing the updated details of the interview session.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateInterviewSessionAsync(InterviewSession interviewSession);

        /// <summary>
        /// Retrieves the interview session that is adjacent to the current session for the specified user.
        /// </summary>
        /// <remarks>This method is intended to be used in scenarios where interview sessions are
        /// sequentially linked  for a user. The definition of "adjacent" may depend on the implementation, such as the
        /// next or previous session in chronological order.</remarks>
        /// <param name="userID">The unique identifier of the user whose adjacent interview session is to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the adjacent  <see
        /// cref="InterviewSession"/> for the specified user, or <see langword="null"/> if no adjacent session exists.</returns>
        Task<InterviewSession> GetAdjacentInterviewSessionAsync(int userID);

        /// <summary>
        /// Retrieves all interviews associated with a specific user by their unique identifier.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="InterviewSession"/> objects.</returns>
        Task<List<InterviewSession>> GetInterviewSessionsByUserIdAsync(int userID);

        /// <summary>
        /// Deletes the specified interview sessions asynchronously.
        /// </summary>
        /// <param name="interviewSessions">A list of <see cref="InterviewSession"/> objects representing the interview sessions to delete.</param>
        Task DeleteInterviewSessionsAsync(List<InterviewSession> interviewSessions);
    }
}
