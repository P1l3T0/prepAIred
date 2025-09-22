using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Repository interface for handling user data persistence operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves the currently authenticated user's information.
        /// </summary>
        /// <returns>A DTO containing the current user's public information.</returns>
        Task<CurrentUserDTO> GetCurrentUserAsync();

        /// <summary>
        /// Deletes the currently authenticated user from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCurrentUserAsync();
    }
}
