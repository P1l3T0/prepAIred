namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for representing an interview session.
    /// </summary>
    public class InterviewSessionDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the ID of the user who owns this interview session.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the topic associated with the current context.
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the interview session has been completed.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the AI agent used for this interview session.
        /// </summary>
        public string AIAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the score or rating assigned to this interview session.
        /// </summary>
        public string Score { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of interviews that are part of this session.
        /// </summary>
        public List<InterviewDTO> Interviews { get; set; } = new List<InterviewDTO>();
    }
}
