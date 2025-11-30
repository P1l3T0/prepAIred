using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for handling authentication business logic.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with hashed credentials.
        /// </summary>
        /// <param name="userCredentialsDto">The registration details.</param>
        /// <param name="hashedPassword">The pre-hashed password.</param>
        /// <param name="saltPassword">The salt used in password hashing.</param>
        /// <returns>The newly created user entity.</returns>
        Task<CurrentUserDTO> RegisterAsync(UserCredentialsDTO userCredentialsDto, byte[] hashedPassword, byte[] saltPassword);

        /// <summary>
        /// Authenticates a user and returns their information.
        /// </summary>
        /// <param name="loginDto">The login credentials.</param>
        /// <returns>The authenticated user entity.</returns>
        Task<CurrentUserDTO> LoginAsync(LoginDTO loginDto);

        /// <summary>
        /// Logs out the current user and cleans up their session.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Logout();

        /// <summary>
        /// Generates authentication response for a user.
        /// </summary>
        /// <param name="currentUser">The user to generate authentication response for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task GenerateAuthResponse(CurrentUserDTO currentUser);
    }
}
