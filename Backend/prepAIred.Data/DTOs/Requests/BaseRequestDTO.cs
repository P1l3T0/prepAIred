namespace prepAIred.Data
{
    public class BaseRequestDTO
    {
        /// <summary>
        /// Gets or sets the name of the AI agent to use for generating interview questions (e.g., ChatGPT, Claude, Gemini).
        /// </summary>
        public string AIAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of interview questions to generate.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
