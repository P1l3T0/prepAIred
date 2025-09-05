using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Repository interface for AI interview data operations.
    /// </summary>
    public interface IAIRepository
    {
        /// <summary>
        /// Creates a new AI-generated interview based on the request parameters.
        /// </summary>
        /// <param name="aIRequest">The request DTO containing interview parameters.</param>
        Task CreateAiInterview(AIRequestDTO aIRequest);

        /// <summary>
        /// Retrieves a list of AI-generated interviews.
        /// </summary>
        /// <returns>List of InterviewDTO objects.</returns>
        Task<List<InterviewDTO>> GetAiInterview();
    }
}
