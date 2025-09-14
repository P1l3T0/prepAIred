namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for user registration data.
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Gets or sets the email address for the new user account.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username for the new user account.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new user account.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
