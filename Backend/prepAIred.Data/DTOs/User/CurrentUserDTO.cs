namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object representing the currently authenticated user's public information.
    /// </summary>
    public class CurrentUserDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
