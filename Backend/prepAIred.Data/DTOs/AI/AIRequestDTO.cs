namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for the AI request.
    /// </summary>
    public class AIRequestDTO
    {
        /// <summary>
        /// Gets or sets the name of the AI agent.
        /// </summary>
        public string AIAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the level associated with the current context.
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of questions in the current context.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
