using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Defines methods for performing data operations related to AI-generated interviews.
    /// </summary>
    public interface IInterviewRepository
    {
        /// <summary>
        /// Generates and stores a new set of AI-generated interviews based on the specified request parameters.
        /// </summary>
        /// <param name="aIRequest">The request DTO containing parameters such as agent, topic, level, and number of questions.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task GenerateAiInterviews(AIRequestDTO aIRequest);

        /// <summary>
        /// Retrieves all AI-generated interviews currently stored in the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="InterviewDTO"/> objects.</returns>
        Task<List<InterviewDTO>> GetAiInterview();
    }
}
