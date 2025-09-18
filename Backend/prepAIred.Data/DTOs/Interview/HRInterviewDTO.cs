namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for representing an HR interview question with soft skills focus and behavioral context.
    /// </summary>
    public class HRInterviewDTO : InterviewDTO
    {
        /// <summary>
        /// Gets or sets the primary soft skill focus area.
        /// </summary>
        public string SoftSkillFocus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the competency area being assessed.
        /// </summary>
        public string CompetencyArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the behavioral context of the interview question.
        /// </summary>
        public string BehavioralContext { get; set; } = string.Empty;
    }
}
