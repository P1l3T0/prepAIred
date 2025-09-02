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
        /// Updates a user's information in the database.
        /// </summary>
        /// <param name="userID">The ID of the user to update.</param>
        /// <returns>The updated user entity.</returns>
        Task<User> UpdateUserAsync(int userID);

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userID">The ID of the user to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteUser(int userID);
    }
}
