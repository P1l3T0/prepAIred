using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Defines methods for performing data operations related to AI-generated interviews.
    /// </summary>
    public interface IInterviewRepository
    {
        /// <summary>
        /// Generates a collection of interviews based on the specified AI request.
        /// </summary>
        /// <typeparam name="TInterview">The type of the interview objects to generate. Must be a reference type.</typeparam>
        /// <param name="aIRequest">The AI request containing the parameters and context for generating interviews.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task GenerateInterviewsAsync<TInterview>(BaseRequestDTO aIRequest) where TInterview : Interview;

        /// <summary>
        /// Retrieves the most recent interview record and maps it to the specified DTO type.
        /// </summary>
        /// <typeparam name="TInterview">The type representing the interview entity in the data source.</typeparam>
        /// <typeparam name="TInterviewDTO">The type to which the interview entity will be mapped.</typeparam>
        /// <returns>A task that represents the asynchronous operation. The task result contains the most recent interview 
        /// mapped to the specified DTO type, or <see langword="null"/> if no interviews are available.</returns>
        Task<List<TInterviewDTO>> GetLatestInterviews<TInterview, TInterviewDTO>() 
            where TInterview : Interview 
            where TInterviewDTO : InterviewDTO;
    }
}
