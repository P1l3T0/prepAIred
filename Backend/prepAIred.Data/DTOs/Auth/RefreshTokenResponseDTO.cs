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
        /// <value>The new access token to be used for authentication.</value>
        public string NewAccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new refresh token.
        /// </summary>
        /// <value>The new refresh token for obtaining future access tokens.</value>
        public string NewRefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username associated with these tokens.
        /// </summary>
        /// <value>The username of the authenticated user.</value>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of seconds until the access token expires.
        /// </summary>
        /// <value>The token expiration time in seconds.</value>
        public int ExpiresIn { get; set; }
    }
}
