namespace prepAIred.Data
{
    /// <summary>
    /// Data transfer object containing statistical information about a user's interview performance.
    /// </summary>
    public class ProfileStatisticsDTO
    {
        /// <summary>
        /// Gets or sets the total number of interview sessions completed by the user.
        /// </summary>
        public int TotalInterviewSessions { get; set; }

        /// <summary>
        /// Gets or sets the number of interview sessions that the user has passed.
        /// </summary>
        public int PassedInterviewSessions { get; set; }

        /// <summary>
        /// Gets or sets the number of interview sessions that are currently ongoing.
        /// </summary>
        public int OngoingInterviewSessions { get; set; }

        /// <summary>
        /// Gets or sets the average score achieved across all completed interview sessions.
        /// </summary>
        public decimal AverageScore { get; set; }

        /// <summary>
        /// Gets or sets the completion rate as a percentage of finished interviews out of total started interviews.
        /// </summary>
        public decimal CompletionRate { get; set; }
    }
}
