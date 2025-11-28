namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for representing a recent activity/interview session summary.
    /// </summary>
    public class InterviewSessionActivityDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the subject or topic of the interview session.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the average score achieved in the interview session.
        /// </summary>
        public float AverageScore { get; set; }

        /// <summary>
        /// Gets or sets the AI agent used for the interview session.
        /// </summary>
        public string AiAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of interview types included in the session.
        /// </summary>
        public List<string> InterviewTypes { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the status of the interview session (Passed, Failed, Ongoing).
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}