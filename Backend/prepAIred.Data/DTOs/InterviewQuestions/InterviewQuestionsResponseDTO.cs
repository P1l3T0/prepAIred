namespace prepAIred.Data
{
    public class InterviewQuestionsResponseDTO
    {
        public int ID { get; set; }
        public string Question { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new List<string>();
    }
}
