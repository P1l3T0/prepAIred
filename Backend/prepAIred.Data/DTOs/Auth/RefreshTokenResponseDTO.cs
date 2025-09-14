namespace prepAIred.Data
{
    /// <summary>
    /// Data Transfer Object for refresh token response containing new authentication tokens.
    /// </summary>
    public class RefreshTokenResponseDTO
    {
        /// <summary>
        /// Gets or sets the new JWT access token.
        /// </summary>
        public string NewAccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new refresh token.
        /// </summary>
        public string NewRefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username associated with these tokens.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of seconds until the access token expires.
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
