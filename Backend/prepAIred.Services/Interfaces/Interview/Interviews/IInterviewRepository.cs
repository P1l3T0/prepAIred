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
        Task GenerateInterviewsAsync<TInterview>(BaseRequestDTO aIRequest) where TInterview : class;
    }
}
