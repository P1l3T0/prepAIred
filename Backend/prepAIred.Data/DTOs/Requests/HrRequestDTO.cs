namespace prepAIred.Data
{
    public class HrRequestDTO : BaseRequestDTO
    {
        /// <summary>
        /// Gets or sets the primary soft skill focus area.
        /// </summary>
        public List<string> SoftSkillFocus { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the scenario context as a string.
        /// </summary>
        public string ContextScenario { get; set; } = string.Empty;
    }
}
