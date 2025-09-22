using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Defines methods for managing and retrieving interview data.
    /// </summary>
    public interface IInterviewService
    {
        /// <summary>
        /// Creates and persists multiple interview entities in the database for a given user and session.
        /// </summary>
        /// <param name="interviews">The list of <see cref="Interview"/> entities to create.</param>
        /// <param name="currentUser">The user for whom the interviews are being created.</param>
        /// <param name="interviewSession">The interview session to which the interviews belong.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateInterviewsAsync(List<Interview> interviews, User currentUser, InterviewSession interviewSession);

        /// <summary>
        /// Retrieves a list of interviews associated with the specified session ID.
        /// </summary>
        /// <typeparam name="TInterview">The type of interview to retrieve. Must derive from the <see cref="Interview"/> class.</typeparam>
        /// <param name="sessionID">The unique identifier of the session for which interviews are to be retrieved. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of interviews of type
        /// <typeparamref name="TInterview"/> associated with the specified session ID. If no interviews are found, the list will be empty.</returns>
        Task<List<TInterview>> GetInterviewsBySessionIdAsync<TInterview>(int sessionID) where TInterview : Interview;
    }
}