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
    }
}
