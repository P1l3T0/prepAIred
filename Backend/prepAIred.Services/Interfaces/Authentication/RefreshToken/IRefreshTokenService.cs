using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for managing refresh token operations.
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Adds a new refresh token to the system.
        /// </summary>
        /// <param name="refreshToken">The refresh token to add.</param>
        /// <returns>The added refresh token entity.</returns>
        Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Retrieves a refresh token by its token string.
        /// </summary>
        /// <param name="refreshToken">The token string to search for.</param>
        /// <returns>The matching refresh token entity if found.</returns>
        Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Retrieves a refresh token by user ID.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>The refresh token entity associated with the user if found.</returns>
        Task<RefreshToken> GetRefreshTokenByUserIdAsync(int userID);
    }
}
