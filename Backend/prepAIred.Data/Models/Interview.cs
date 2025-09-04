using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    public class Interview : BaseModel
    {
        public int UserID { get; set; }
        public string Question { get; set; } = string.Empty;
        public bool IsAnswered { get; set; }
        public string AnswersJson { get; set; } = "[]";

        [NotMapped]
        public List<string> Answers
        {
            get => JsonSerializer.Deserialize<List<string>>(AnswersJson) ?? new List<string>();
            set => AnswersJson = JsonSerializer.Serialize(value);
        }

        [JsonIgnore]
        public User? User { get; set; }

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
