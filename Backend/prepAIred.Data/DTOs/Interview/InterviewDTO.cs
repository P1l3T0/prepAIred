using System.Text.Json.Serialization;

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
        /// <value>The question text presented in the interview.</value>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the interview question has been answered.
        /// This property is ignored during JSON serialization.
        /// </summary>
        /// <value><c>true</c> if the question has been answered; otherwise, <c>false</c>.</value>
        [JsonIgnore]
        public bool IsAnswered { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of answers provided for the interview question.
        /// </summary>
        /// <value>A list of answer strings for the interview question.</value>
        public List<string> Answers { get; set; } = new List<string>();
    }
}
