using Microsoft.AspNetCore.Http;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for managing profile picture operations including file storage, retrieval, and deletion.
    /// Coordinates between file system operations and database updates for profile picture management.
    /// </summary>
    public interface IProfilePictureService
    {
        /// <summary>
        /// Saves an uploaded image file to the file system and returns the file name.
        /// </summary>
        /// <param name="imageFile">The uploaded image file to save</param>
        /// <returns>The saved file name with extension</returns>
        Task<string> SaveFileAsync(IFormFile imageFile);
        
        /// <summary>
        /// Retrieves the profile picture URL for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>The URL path to the user's profile picture</returns>
        Task<string> GetProfilePictureUrlByUserIdAsync(int userId);
        
        /// <summary>
        /// Deletes a profile picture file from the file system.
        /// </summary>
        /// <param name="fileNameWithExtension">The complete file name including extension to delete</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task DeleteProfilePictureAsync(string fileNameWithExtension);
    }
}
