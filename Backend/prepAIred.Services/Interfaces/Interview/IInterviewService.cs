using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for interview data management and retrieval.
    /// </summary>
    public interface IInterviewService
    {
        /// <summary>
        /// Creates multiple interview entities in the database.
        /// </summary>
        /// <param name="interviews">List of Interview entities to create.</param>
        Task CreateInterviewsAsync(List<Interview> interviews, User currentUser, InterviewSession interviewSession);

        /// <summary>
        /// Retrieves interviews for a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>List of InterviewDTO objects.</returns>
        Task<List<InterviewDTO>> GetInterviewsByUserIdAsync(int userId);
    }
}