using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for interview questions and answers.
    /// </summary>
    public class InterviewDTO : BaseDTO
    {
        /// <summary>
        /// The interview question text.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the question has been answered.
        /// </summary>
        [JsonIgnore]
        public bool IsAnswered { get; set; } = false;

        /// <summary>
        /// List of answers for the interview question.
        /// </summary>
        public List<string> Answers { get; set; } = new List<string>();
    }
}
