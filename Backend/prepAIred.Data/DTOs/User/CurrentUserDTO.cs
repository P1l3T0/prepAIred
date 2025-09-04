using System.ComponentModel.DataAnnotations;

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
        /// Gets or sets the hashed password for the user.
        /// </summary>
        /// <value>The password hash byte array used for secure authentication.</value>
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets or sets the salt used in password hashing.
        /// </summary>
        /// <value>The password salt byte array used in the hashing process.</value>
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        /// <value>The timestamp of account creation.</value>
        public DateTime DateCreated { get; set; }
    }
}
