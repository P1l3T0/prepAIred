namespace prepAIred.Data
{
    public class InterviewSession : BaseModel
    {
        public int UserID { get; set; }
        public string Topic { get; set; } = string.Empty;
        public AIAgent AIAgent { get; set; } = AIAgent.ChatGPT;
        public InterviewSessionScore Score { get; set; } = InterviewSessionScore.Good;
        public User? User { get; set; }
        public List<Interview> Interviews { get; set; } = new List<Interview>();

        public override T ToDto<T>()
        {
            InterviewSessionDTO interviewSessionDTO = new InterviewSessionDTO()
            {
                UserID = UserID,
                AIAgent = AIAgent,
                Score = Score,
                Interviews = Interviews.ConvertAll(interview => interview.ToDto<InterviewDTO>()),
            };

            return (T)(object)interviewSessionDTO;
        }
    }
}
