using System.Diagnostics.CodeAnalysis;

namespace prepAIred.Data
{
    /// <summary>
    /// Represents an Interview Session for a user, including data such as topic, AI agent, and score.
    /// </summary>
    public class InterviewSession : BaseModel
    {
        /// <summary>
        /// Gets or sets the ID of the user who owns this interview session.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the main topic or subject of the interview session.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the AI agent used to conduct or assist with the interview session.
        /// </summary>
        public AIAgent AIAgent { get; set; } = AIAgent.ChatGPT;

        /// <summary>
        /// Gets or sets the HR score for this interview session.
        /// </summary>
        public float HrScore { get; set; } = 0;

        /// <summary>
        /// Gets or sets the technical score for this interview session.
        /// </summary>
        public float TechnicalScore { get; set; } = 0;

        /// <summary>
        /// Gets the overall score or rating assigned to this interview session.
        /// </summary>
        [AllowNull]
        public float AverageScore => (float)Math.Round((HrScore + TechnicalScore) / 2, 2);

        /// <summary>
        /// Gets or sets the current status of the interview session.
        /// </summary>
        public InterviewSessionStatus Status { get; set; } = InterviewSessionStatus.Failed;

        /// <summary>
        /// Gets or sets the user entity associated with this session (navigation property).
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the collection of interviews that are part of this session.
        /// </summary>
        public List<Interview> Interviews { get; set; } = new List<Interview>();

        /// <summary>
        /// Converts this InterviewSession entity to its corresponding DTO representation.
        /// </summary>
        /// <typeparam name="T">The type of DTO to convert to.</typeparam>
        /// <returns>The DTO representation of this interview session.</returns>
        public override T ToDto<T>()
        {
            InterviewSessionDTO interviewSessionDTO = new InterviewSessionDTO()
            {
                ID = ID,
                UserID = UserID,
                Subject = Subject,
                AverageScore = AverageScore,
                AIAgent = AIAgent.ToString(),
                Interviews = Interviews.ConvertAll(i => i.ToDto<InterviewDTO>()),
                DateCreated = DateCreated,
            };

            return (T)(object)interviewSessionDTO;
        }
    }
}
