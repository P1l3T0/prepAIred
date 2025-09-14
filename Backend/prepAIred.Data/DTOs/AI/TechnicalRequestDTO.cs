namespace prepAIred.Data
{
    /// <summary>
    /// Represents a data transfer object for specifying the technical requirements of an interview request.
    /// </summary>
    /// <remarks>This class is used to encapsulate the topic and difficulty level for generating interview
    /// questions. It extends the <see cref="BaseRequestDTO"/> class, inheriting any shared properties or
    /// functionality.</remarks>
    public class TechnicalRequestDTO : BaseRequestDTO
    {
        /// <summary>
        /// Gets or sets the main topic or subject area for the interview questions (e.g., Software Engineering).
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the difficulty level for the interview questions (e.g., Junior, Senior).
        /// </summary>
        public string Level { get; set; } = string.Empty;
    }
}
