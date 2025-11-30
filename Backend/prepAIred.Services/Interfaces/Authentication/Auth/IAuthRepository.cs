using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Repository interface for handling authentication-related data operations.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userCredentialsDto">The registration data transfer object containing user details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RegisterAsync(UserCredentialsDTO userCredentialsDto);

        /// <summary>
        /// Authenticates a user using their credentials.
        /// </summary>
        /// <param name="loginDto">The login data transfer object containing user credentials.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task LoginAsync(LoginDTO loginDto);

        /// <summary>
        /// Logs out the current user from the system.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Logout();
    }
}
