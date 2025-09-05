using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    /// <summary>
    /// Entity representing an interview question and its answers for a user.
    /// </summary>
    public class Interview : BaseModel
    {
        /// <summary>
        /// The ID of the user associated with this interview.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The interview question text.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the question has been answered.
        /// </summary>
        public bool IsAnswered { get; set; }

        /// <summary>
        /// JSON representation of the answers list.
        /// </summary>
        public string AnswersJson { get; set; } = "[]";

        /// <summary>
        /// List of answers for the interview question (not mapped to DB).
        /// </summary>
        [NotMapped]
        public List<string> Answers
        {
            get => JsonSerializer.Deserialize<List<string>>(AnswersJson) ?? new List<string>();
            set => AnswersJson = JsonSerializer.Serialize(value);
        }

        /// <summary>
        /// The user entity associated with this interview (navigation property).
        /// </summary>
        [JsonIgnore]
        public User? User { get; set; }

        /// <summary>
        /// Converts the Interview entity to its DTO representation.
        /// </summary>
        public override T ToDto<T>()
        {
            InterviewDTO interviewDTO = new InterviewDTO()
            {
                ID = ID,
                DateCreated = DateCreated,
                Question = Question,
                Answers = Answers,
                IsAnswered = IsAnswered
            };

            return (T)(object)interviewDTO;
        }
    }
}
