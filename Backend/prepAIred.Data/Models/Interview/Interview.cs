using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace prepAIred.Data
{
    /// <summary>
    /// Represents an interview question, its answers, and related user/session information.
    /// </summary>
    public class Interview : BaseModel
    {
        /// <summary>
        /// Gets or sets the ID of the user to whom this interview belongs.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the interview session this interview is part of.
        /// </summary>
        public int InterviewSessionID { get; set; }

        /// <summary>
        /// Gets or sets the text of the interview question.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the interview question has been answered.
        /// </summary>
        public bool IsAnswered { get; set; }

        /// <summary>
        /// Gets or sets the JSON-serialized list of answers for this interview question.
        /// Used for database storage.
        /// </summary>
        public string AnswersJson { get; set; } = "[]";

        /// <summary>
        /// Gets or sets the list of answers for the interview question.
        /// This property is not mapped to the database directly.
        /// </summary>
        [NotMapped]
        public List<string> Answers
        {
            get => JsonSerializer.Deserialize<List<string>>(AnswersJson) ?? new List<string>();
            set => AnswersJson = JsonSerializer.Serialize(value);
        }

        /// <summary>
        /// Gets or sets the user entity associated with this interview (navigation property).
        /// </summary>
        [JsonIgnore]
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the interview session entity associated with this interview (navigation property).
        /// </summary>
        [JsonIgnore]
        public InterviewSession? InterviewSession { get; set; }

        /// <summary>
        /// Converts this Interview entity to its corresponding DTO representation.
        /// </summary>
        /// <typeparam name="T">The type of DTO to convert to.</typeparam>
        /// <returns>The DTO representation of this interview.</returns>
        public override T ToDto<T>()
        {
            InterviewDTO interviewDTO = new InterviewDTO()
            {
                ID = ID,
                UserID = UserID,
                InterviewSessionID = InterviewSessionID,
                DateCreated = DateCreated,
                Question = Question,
                Answers = Answers,
                IsAnswered = IsAnswered
            };

            return (T)(object)interviewDTO;
        }
    }
}
