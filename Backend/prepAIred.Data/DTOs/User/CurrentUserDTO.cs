namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object representing the currently authenticated user's public information.
    /// </summary>
    public class CurrentUserDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        /// <value>The user's database ID.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>The email address associated with the user account.</value>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        /// <value>The username chosen by the user.</value>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        /// <value>The timestamp of account creation.</value>
        public DateTime DateCreated { get; set; }
    }
}
