using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Repository interface for managing refresh token data operations.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Generates a new refresh token and its associated response.
        /// </summary>
        /// <returns>A response containing the new refresh token and access token details.</returns>
        Task<RefreshTokenResponseDTO> GenerateNewRefreshTokenAsync();
    }
}
