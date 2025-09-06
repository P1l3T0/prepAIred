namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for specifying parameters when requesting AI-generated interview questions.
    /// </summary>
    public class AIRequestDTO
    {
        /// <summary>
        /// Gets or sets the name of the AI agent to use for generating interview questions (e.g., ChatGPT, Claude, Gemini).
        /// </summary>
        /// <value>The name of the AI agent to use for question generation.</value>
        public string AIAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the main topic or subject area for the interview questions (e.g., Software Engineering).
        /// </summary>
        /// <value>The topic or subject area for the interview questions.</value>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the difficulty level for the interview questions (e.g., Junior, Senior).
        /// </summary>
        /// <value>The difficulty level for the interview questions.</value>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of interview questions to generate.
        /// </summary>
        /// <value>The number of questions to generate for the interview.</value>
        public int NumberOfQuestions { get; set; }
    }
}
