namespace prepAIred.Data
{
    public class InterviewSession : BaseModel
    {
        public int InterviewID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; } 
        public List<Interview> Interviews { get; set; } = new List<Interview>();

        public override T ToDto<T>()
        {
            InterviewSessionDTO interviewSessionDTO = new InterviewSessionDTO()
            {
                ID = ID,
                InterviewID = InterviewID,
                UserID = UserID,
                Interviews = Interviews.ConvertAll(interview => interview.ToDto<InterviewDTO>()),
                DateCreated = DateCreated,
            };

            return (T)(object)interviewSessionDTO;
        }
    }
}
