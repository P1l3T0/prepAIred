using System.IdentityModel.Tokens.Jwt;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for JWT (JSON Web Token) operations.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a new JWT access token for a user.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>The generated access token string.</returns>
        string GenerateAcessToken(int userID);

        /// <summary>
        /// Generates a new refresh token for a user.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>The generated refresh token string.</returns>
        string GenerateRefreshToken(int userID);

        /// <summary>
        /// Extracts the user ID from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token to analyze.</param>
        /// <returns>The user ID from the token claims.</returns>
        string GetUserIdFromToken(JwtSecurityToken token);

        /// <summary>
        /// Verifies a JWT token and returns its decoded form.
        /// </summary>
        /// <param name="jwtToken">The JWT token string to verify.</param>
        /// <returns>The decoded JWT security token if valid.</returns>
        JwtSecurityToken Verify(string jwtToken);
    }
}
