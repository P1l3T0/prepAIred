using System.Text.Json.Serialization;

namespace prepAIred.Data
{
    /// <summary>
    /// Represents a refresh token used for maintaining user sessions.
    /// </summary>
    public class RefreshToken : BaseModel
    {
        /// <summary>
        /// Gets or sets the ID of the user this refresh token belongs to.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the token string value.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the token has been revoked.
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token expires.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the associated user for this refresh token.
        /// </summary>
        [JsonIgnore]
        public User? User { get; set; }
    }
}
