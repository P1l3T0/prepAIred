using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using prepAIred.Data;
using prepAIred.Exceptions;

namespace prepAIred.Services
{
    public class ProfilePictureService(IFileService fileService, DataContext dataContent) : IProfilePictureService
    {
        private readonly IFileService _fileService = fileService;
        private readonly DataContext _dataContext = dataContent;

        public async Task<string> SaveFileAsync(IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                throw new ProfilePictureException("File is null or empty");
            }

            string path = _fileService.CreateDirectoryIfNotExists();
            string extension = _fileService.CheckFileExtension(imageFile);
            string fileName = await _fileService.CreateFileName(imageFile, path, extension);

            return fileName;
        }

        public async Task<string> GetProfilePictureUrlByUserIdAsync(int userId)
        {
            string profilePictureName = await _dataContext.Users
                .Where(u => u.ID == userId)
                .Select(u => u.ProfilePicture)
                .FirstOrDefaultAsync() ?? string.Empty;

            string fullPath = $"https://localhost:7227/Uploads/{profilePictureName}";

            return fullPath;
        }

        public async Task DeleteProfilePictureAsync(string fileNameWithExtension)
        {
            string fullPath = _fileService.GetFullPathOfProfilePicture(fileNameWithExtension);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            await _dataContext.Users
                .Where(u => u.ProfilePicture == fileNameWithExtension)
                .ForEachAsync(u => u.ProfilePicture = string.Empty);
        }
    }
}

