namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for representing a technical interview question with programming language and difficulty specifications.
    /// </summary>
    public class TechnicalInterviewDTO : InterviewDTO
    {
        /// <summary>
        /// Gets or sets the programming language that is the focus of the technical interview.
        /// </summary>
        public string ProgrammingLanguage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the difficulty level of the technical interview questions.
        /// </summary>
        public string DifficultyLevel { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the specific technical topic area being covered in the interview.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the position or title associated with an entity.
        /// </summary>
        public string Position { get; set; } = string.Empty;
    }
}
