namespace prepAIred.Data
{
    /// <summary>
    /// Represents the possible status values for an interview session.
    /// </summary>
    public enum InterviewSessionStatus
    {
        /// <summary>
        /// Indicates that the interview session is currently in progress.
        /// </summary>
        Ongoing,

        /// <summary>
        /// Indicates that the interview session has been completed successfully and the candidate passed.
        /// </summary>
        Passed,

        /// <summary>
        /// Indicates that the interview session has been completed but the candidate did not pass.
        /// </summary>
        Failed
    }
}
