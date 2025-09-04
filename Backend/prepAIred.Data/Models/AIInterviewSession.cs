using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    public class AIInterviewSession : BaseModel
    {
        public int UserID { get; set; }

        public List<InterviewQuestionsResponseDTO> Questions { get; set; } = new List<InterviewQuestionsResponseDTO>();

        [JsonIgnore]
        public User? User { get; set; }
    }
}
