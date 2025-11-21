using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Repository interface for managing profile picture data operations in the database.
    /// Handles retrieval and updates of user profile picture information.
    /// </summary>
    public interface IProfilePictureRepository
    {
        /// <summary>
        /// Retrieves the profile picture URL for the current authenticated user.
        /// </summary>
        /// <returns>The URL path to the user's profile picture</returns>
        Task<string> GetProfilePictureUrlAsync();
        
        /// <summary>
        /// Updates the user's profile picture information in the database.
        /// </summary>
        /// <param name="profilePictureDTO">Data transfer object containing profile picture details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ChangeProfilePictureAsync(ProfilePictureDTO profilePictureDTO);
    }
}
