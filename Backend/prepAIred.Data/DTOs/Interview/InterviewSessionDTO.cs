namespace prepAIred.Data
{
    internal class InterviewSessionDTO : BaseDTO
    {
        public int UserID { get; set; }
        public AIAgent AIAgent { get; set; } = AIAgent.ChatGPT;
        public InterviewSessionScore Score { get; set; } = InterviewSessionScore.NotRated;
        public List<InterviewDTO> Interviews { get; set; } = new List<InterviewDTO>();
    }
}
