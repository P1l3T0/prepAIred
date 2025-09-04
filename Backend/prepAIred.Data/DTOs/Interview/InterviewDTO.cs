namespace prepAIred.Data
{
    public class InterviewDTO : BaseDTO
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new List<string>();
    }
}
