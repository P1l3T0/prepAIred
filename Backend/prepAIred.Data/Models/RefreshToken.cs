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
        /// <value>The foreign key reference to the associated user.</value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the token string value.
        /// </summary>
        /// <value>The unique token string used for authentication.</value>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the token has been revoked.
        /// </summary>
        /// <value>True if the token has been revoked, false otherwise.</value>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token expires.
        /// </summary>
        /// <value>The expiration datetime of the token.</value>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the associated user for this refresh token.
        /// </summary>
        /// <value>The navigation property to the associated User entity.</value>
        [JsonIgnore]
        public User? User { get; set; }
    }
}
