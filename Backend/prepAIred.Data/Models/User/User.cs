using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace prepAIred.Data
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>The email address used for authentication and communication.</value>
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        /// <value>The unique username for the user.</value>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password for the user.
        /// </summary>
        /// <value>The password hash byte array used for secure authentication.</value>
        [Required, MinLength(10), MaxLength(200)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets or sets the salt used in password hashing.
        /// </summary>
        /// <value>The password salt byte array used in the hashing process.</value>
        [Required, MinLength(10), MaxLength(200)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets the collection of refresh tokens associated with the user.
        /// </summary>
        /// <value>A collection of active and expired refresh tokens.</value>
        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();

        /// <summary>
        /// Gets the collection of AI responses associated with the user.
        /// </summary>
        /// <value>A collection of AI-generated questions and answers.</value>
        [JsonIgnore]
        public ICollection<Interview> Interviews { get; } = new List<Interview>();

        /// <summary>
        /// Gets the collection of Interview Sessions associated with the user.
        /// </summary>
        /// <value>A collection of Interview Sessions.</value>
        [JsonIgnore]
        public ICollection<InterviewSession> InterviewSessions { get; } = new List<InterviewSession>();

        /// <summary>
        /// Converts the User model to its DTO representation
        /// </summary>
        /// <typeparam name="T">The type of DTO to convert to, must be CurrentUserDTO</typeparam>
        /// <returns>A CurrentUserDTO containing the user's public information</returns>    
        public override T ToDto<T>()
        {
            CurrentUserDTO currentUser = new CurrentUserDTO()
            {
                ID = ID,
                Email = Email,
                Username = Username,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                DateCreated = DateCreated,
            };

            return (T)(object)currentUser;
        }
    }
}
