using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using prepAIred.Exceptions;

namespace prepAIred.Services
{
    public class FileService(IHostEnvironment hostEnvironment) : IFileService
    {
        private readonly string[] allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

        public async Task<string> CreateFileName(IFormFile imageFile, string path, string extension)
        {
            string fileName = $"{Guid.NewGuid()}{extension}";
            string fileNameWithPath = Path.Combine(path, fileName);

            using FileStream stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return fileName;
        }

        public string GetFullPathOfProfilePicture(string profilePictureName)
        {
            string contentPath = _hostEnvironment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads", profilePictureName);

            return path;
        }

        public string CreateDirectoryIfNotExists()
        {
            string contentPath = _hostEnvironment.ContentRootPath;
            string path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public string CheckFileExtension(IFormFile imageFile)
        {
            string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !allowedFileExtensions.Contains(extension))
            {
                throw new UnsupportedFileExtensionException("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
            }

            return extension;
        }
    }
}
