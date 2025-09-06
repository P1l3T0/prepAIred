namespace prepAIred.Data
{
    /// <summary>
    /// DTO for requesting AI-generated interview questions.
    /// Contains agent, field, level, and number of questions.
    /// </summary>
    public class AIRequestDTO
    {
        /// <summary>
        /// The AI agent to use for generating interview questions (e.g., ChatGPT, Claude, Gemini).
        /// </summary>
        public string AIAgent { get; set; } = string.Empty;

        /// <summary>
        /// The topic for the interview questions (e.g., Software Engineering).
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// The difficulty level for the interview questions (e.g., Junior, Senior).
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// The number of questions to generate for the interview.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
