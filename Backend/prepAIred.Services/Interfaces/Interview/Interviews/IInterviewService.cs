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
        /// Updates multiple interview entities in the database with their current state and evaluation results.
        /// </summary>
        /// <typeparam name="TInterview">The type of interview objects to update. Must derive from the Interview class.</typeparam>
        /// <param name="interviews">The list of interview objects containing updated information to persist to the database.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateInterviewAsync<TInterview>(List<TInterview> interviews) where TInterview : Interview;

        /// <summary>
        /// Retrieves a list of interviews associated with the specified session ID.
        /// </summary>
        /// <typeparam name="TInterview">The type of interview to retrieve. Must derive from the <see cref="Interview"/> class.</typeparam>
        /// <param name="sessionID">The unique identifier of the session for which interviews are to be retrieved. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of interviews of type
        /// <typeparamref name="TInterview"/> associated with the specified session ID. If no interviews are found, the list will be empty.</returns>
        Task<List<TInterview>> GetInterviewsBySessionIdAsync<TInterview>(int sessionID) where TInterview : Interview;

        /// <summary>
        /// Retrieves the latest interviews from the provided list and maps them to the specified DTO type.
        /// </summary>
        /// <typeparam name="TInterview">The type of the interview entities, which must inherit from <see cref="Interview"/>.</typeparam>
        /// <typeparam name="TInterviewDTO">The type of the interview DTOs, which must inherit from <see cref="InterviewDTO"/>.</typeparam>
        /// <param name="interviews">The list of interview entities to process.</param>
        /// <returns>A list of the latest interviews mapped to the specified DTO type. The list will be empty if no interviews
        /// are provided.</returns>
        List<TInterviewDTO> GetLatestInterviews<TInterview, TInterviewDTO>(List<TInterview> interviews)
            where TInterview : Interview
            where TInterviewDTO : InterviewDTO;

        /// <summary>
        /// Updates an existing interview in the collection with the provided evaluated interview.
        /// </summary>
        /// <typeparam name="TInterview">The type of the interview, which must derive from <see cref="Interview"/>.</typeparam>
        /// <param name="evaluated">The evaluated interview to update the existing interview with.</param>
        /// <param name="existingInterviews">The collection of existing interviews to search for a match to update.</param>
        void UpdateExistingInterviewWithEvaluation<TInterview>(List<TInterview> evaluatedInterviews, List<TInterview> existingInterviews) where TInterview : Interview;
    }
}