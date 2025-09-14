namespace prepAIred.Data
{
    /// <summary>
    /// Represents an HR interview that focuses on evaluating a candidate's soft skills.
    /// </summary>
    /// <remarks>
    /// This class extends the <see cref="Interview"/> class to include additional properties specific to HR interviews, 
    /// such as the primary soft skill focus area, the scenario context, and the evaluation criteria.
    /// </remarks>
    public class HRInterview : Interview
    {
        /// <summary>
        /// Gets or sets the primary soft skill focus area.
        /// </summary>
        public string SoftSkillFocus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the competency area.
        /// </summary>
        public string CompetencyArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the behavioral context.
        /// </summary>
        public string BehavioralContext { get; set; } = string.Empty;
    }
}
