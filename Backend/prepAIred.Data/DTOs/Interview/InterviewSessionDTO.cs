namespace prepAIred.Data
{
    internal class InterviewSessionDTO : BaseDTO
    {
        public int InterviewID { get; set; }
        public int UserID { get; set; }
        public List<InterviewDTO> Interviews { get; set; } = new List<InterviewDTO>();
    }
}
