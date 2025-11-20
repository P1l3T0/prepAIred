using Microsoft.AspNetCore.Http;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for handling file operations including validation, storage, and path management.
    /// Provides methods for file name generation, directory creation, and file extension validation.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Creates a unique file name for the uploaded image file.
        /// </summary>
        /// <param name="imageFile">The uploaded image file</param>
        /// <param name="path">The directory path where the file will be stored</param>
        /// <param name="extension">The file extension to use</param>
        /// <returns>A unique file name with the specified extension</returns>
        Task<string> CreateFileName(IFormFile imageFile, string path, string extension);
        
        /// <summary>
        /// Gets the full file system path for a profile picture.
        /// </summary>
        /// <param name="profilePictureName">The name of the profile picture file</param>
        /// <returns>The complete file path to the profile picture</returns>
        string GetFullPathOfProfilePicture(string profilePictureName);
        
        /// <summary>
        /// Creates the upload directory if it doesn't exist and returns the directory path.
        /// </summary>
        /// <returns>The path to the upload directory</returns>
        string CreateDirectoryIfNotExists();
        
        /// <summary>
        /// Validates and extracts the file extension from an uploaded image file.
        /// </summary>
        /// <param name="imageFile">The uploaded image file to check</param>
        /// <returns>The validated file extension</returns>
        string CheckFileExtension(IFormFile imageFile);
    }
}
