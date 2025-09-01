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
        /// <value>The email address used for registration and future authentication.</value>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username for the new user account.
        /// </summary>
        /// <value>The display name chosen by the user.</value>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new user account.
        /// </summary>
        /// <value>The plain text password (will be hashed before storage).</value>
        public string Password { get; set; } = string.Empty;
    }
}
