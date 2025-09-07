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
        /// Gets or sets the AI agent used for this interview session.
        /// </summary>
        public AIAgent AIAgent { get; set; } = AIAgent.ChatGPT;

        /// <summary>
        /// Gets or sets the score or rating assigned to this interview session.
        /// </summary>
        public InterviewSessionScore Score { get; set; } = InterviewSessionScore.NotRated;

        /// <summary>
        /// Gets or sets the list of interviews that are part of this session.
        /// </summary>
        public List<InterviewDTO> Interviews { get; set; } = new List<InterviewDTO>();
    }
}
