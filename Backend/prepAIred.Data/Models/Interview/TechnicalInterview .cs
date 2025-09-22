namespace prepAIred.Data
{
    /// <summary>
    /// Represents a technical interview that focuses on assessing a candidate's proficiency in a specific programming language, difficulty level, and technical topic area.
    /// </summary>
    /// <remarks>
    /// This class extends the <see cref="Interview"/> class to include properties specific to technical interviews, 
    /// such as the programming language, difficulty level, and topic area. It is designed to capture the key details of a technical interview session.
    /// </remarks>
    public class TechnicalInterview : Interview
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

        public override T ToDto<T>()
        {
            TechnicalInterviewDTO technicalInterviewDTO = new TechnicalInterviewDTO()
            {
                ID = ID,
                DateCreated = DateCreated,
                UserID = UserID,
                InterviewSessionID = InterviewSessionID,
                Question = Question,
                IsAnswered = IsAnswered,
                Answers = Answers,
                QuestionType = QuestionType.ToString(),
                InterviewType = InterviewType.ToString(),
                ProgrammingLanguage = ProgrammingLanguage,
                DifficultyLevel = DifficultyLevel,
                Subject = Subject,
                Position = Position
            };

            return (T)(object)technicalInterviewDTO;
        }
    }
}