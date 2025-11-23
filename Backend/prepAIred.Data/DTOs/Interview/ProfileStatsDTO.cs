namespace prepAIred.Data
{
    public class ProfileStatisticsDTO
    {
        public int TotalInterviewSessions { get; set; }
        public int PassedInterviewSessions { get; set; }
        public int OngoingInterviewSessions { get; set; }
        public decimal AverageScore { get; set; }
        public decimal CompletionRate { get; set; }
    }
}
