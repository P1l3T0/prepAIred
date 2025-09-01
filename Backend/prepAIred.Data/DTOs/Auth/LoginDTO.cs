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
        /// <value>The user's email address.</value>
        public string Email { get; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        /// <value>The user's plain text password (should never be stored).</value>
        public string Password { get; } = string.Empty;
    }
}
