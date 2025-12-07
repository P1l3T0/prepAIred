namespace prepAIred.Data
{
    public class ProgrammingLanguageDataDTO
    {
        /// <summary>
        /// Gets or sets the programming language name.
        /// </summary>
        public string Language { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of sessions for the programming language.
        /// </summary>
        public int Sessions { get; set; }
    }
}
