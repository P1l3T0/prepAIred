namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for representing an interview question and its associated answers.
    /// </summary>
    public class InterviewDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the text of the interview question.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the question.
        /// </summary>
        public string QuestionType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the interview question has been answered.
        /// </summary>
        public bool IsAnswered { get; set; } = false;

        /// <summary>
        /// Gets or sets the answer selected by the user for this question.
        /// </summary>
        public string SelectedAnswer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the score assigned to this question.
        /// </summary>
        public float Score { get; set; } = 0;

        /// <summary>
        /// Gets or sets the feedback provided for this question.
        /// </summary>
        public string Feedback { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the current session.
        /// </summary>
        public int InterviewSessionID { get; set; }

        /// <summary>
        /// Gets or sets the type of interview being conducted.
        /// </summary>
        public string InterviewType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of answers provided for the interview question.
        /// </summary>
        public List<string> Answers { get; set; } = new List<string>();
    }
}
