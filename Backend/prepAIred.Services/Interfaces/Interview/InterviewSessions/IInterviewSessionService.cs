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
        /// Retrieves an interview session by its unique identifier.
        /// </summary>
        /// <param name="sessionID">The unique identifier of the interview session to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="InterviewSession"/> object with the specified ID, or <see langword="null"/> if not found.</returns>
        Task<InterviewSession> GetInterviewSessionByIdAsync(int sessionID);

        /// <summary>
        /// Gets an interview session based on the provided evaluation requests.
        /// </summary>
        /// <param name="evaluateRequests">A list of evaluation requests containing the questions and answer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated  
        /// <see cref="InterviewSession"/> object based on the provided evaluation requests.</returns>
        Task<InterviewSession> GetInterviewSessionFromQuestionsAsync(List<EvaluateRequestDTO> evaluateRequests);

        /// <summary>
        /// Retrieves the most recent interview session ID associated with the specified user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user whose latest interview session ID is to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the latest interview session.</returns>
        Task<int> GetLatestInterviewSessionID(int userID);

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

        /// <summary>
        /// Marks an interview session as finished and updates its completion status.
        /// </summary>
        /// <param name="interviewSession">The <see cref="InterviewSession"/> object to finish.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task FinishInterviewSessionAsync(InterviewSession interviewSession);

        /// <summary>
        /// Retrieves the total count of interview sessions associated with a specific user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total number of interview sessions.</returns>
        Task<int> GetTotalInterviewSessionsAsync(int userID);

        /// <summary>
        /// Retrieves the count of passed interview sessions for a specific user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of interview sessions that were passed.</returns>
        Task<int> GetPassedInterviewSessionsAsync(int userID);

        /// <summary>
        /// Retrieves the count of ongoing interview sessions for a specific user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of interview sessions that are currently ongoing.</returns>
        Task<int> GetOngoingInterviewSessionsAsync(int userID);

        /// <summary>
        /// Calculates and retrieves the average score of all interview sessions for a specific user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the average score as a decimal value.</returns>
        Task<decimal> GetAverageScoreAsync(int userID);

        /// <summary>
        /// Calculates and retrieves the completion rate of interview sessions for a specific user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the completion rate as a decimal percentage value.</returns>
        Task<decimal> GetCompletionRateAsync(int userID);

        /// <summary>
        /// Finalizes an interview session by updating its status and associated interviews based on the evaluated interviews
        /// </summary>
        /// <typeparam name="TInterview">The type of interview being finalized.</typeparam>
        /// <param name="interviewSession">The interview session to be finalized.</param>
        /// <param name="evaluatedTInterviews">The list of evaluated interviews to update.</param>
        void FinalizeInterviewSession<TInterview>(InterviewSession interviewSession, List<TInterview> evaluatedTInterviews) where TInterview : Interview;

        /// <summary>
        /// Retrieves recent interview session activities for the user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of recent interview session activities.</returns>
        Task<List<InterviewSessionActivityDTO>> GetInterviewSessionActivitiesAsync(int userID);
    }
}
