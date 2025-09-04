using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    public class InterviewDTO : BaseDTO
    {
        public string Question { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsAnswered { get; set; } = false;
        public List<string> Answers { get; set; } = new List<string>();
    }
}
