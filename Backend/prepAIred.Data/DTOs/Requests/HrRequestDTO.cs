namespace prepAIred.Data
{
    public class HrRequestDTO : BaseRequestDTO
    {
        /// <summary>
        /// Gets or sets the primary soft skill focus area.
        /// </summary>
        public string SoftSkillFocus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scenario context as a string.
        /// </summary>
        public string ContextScenario { get; set; } = string.Empty;
    }
}
