namespace prepAIred.Data
{
    internal class InterviewSession : BaseModel
    {
        public int InterviewId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } 
        public List<Interview> Interviews { get; set; } = new List<Interview>();

        public override T ToDto<T>()
        {
            return base.ToDto<T>();
        }
    }
}
