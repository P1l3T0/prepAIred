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
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the filename of the user's profile picture.
        /// </summary>
        [MaxLength(50)]
        public string ProfilePicture { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password for the user.
        /// </summary>
        [Required, MinLength(10), MaxLength(200)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets or sets the salt used in password hashing.
        /// </summary>
        [Required, MinLength(10), MaxLength(200)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets the collection of refresh tokens associated with the user.
        /// </summary>
        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();

        /// <summary>
        /// Gets the collection of AI responses associated with the user.
        /// </summary>
        [JsonIgnore]
        public ICollection<Interview> Interviews { get; } = new List<Interview>();

        /// <summary>
        /// Gets the collection of Interview Sessions associated with the user.
        /// </summary>
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
                DateCreated = DateCreated,
            };

            return (T)(object)currentUser;
        }
    }
}
