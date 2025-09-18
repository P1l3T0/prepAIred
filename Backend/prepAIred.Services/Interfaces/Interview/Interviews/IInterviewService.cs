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
    }
}