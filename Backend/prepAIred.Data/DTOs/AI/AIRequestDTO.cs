namespace prepAIred.Data
{
    public class AIRequestDTO
    {
        public string AIAgent { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public int NumberOfQuestions { get; set; }
    }
}
