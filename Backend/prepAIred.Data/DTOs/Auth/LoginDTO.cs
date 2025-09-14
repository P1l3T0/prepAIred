namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for user login credentials.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Gets or sets the email address used for authentication.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
