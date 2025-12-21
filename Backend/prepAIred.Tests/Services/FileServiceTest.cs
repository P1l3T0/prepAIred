using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Services
{
    public class FileServiceTest
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly FileService _fileService;
        private readonly string _testContentRoot;

        public FileServiceTest()
        {
            _hostEnvironment = A.Fake<IHostEnvironment>();
            _testContentRoot = Path.Combine(Path.GetTempPath(), "FileServiceTests");
            
            A.CallTo(() => _hostEnvironment.ContentRootPath).Returns(_testContentRoot);

            _fileService = new FileService(_hostEnvironment);
        }

        #region CheckFileExtension Tests

        [Fact]
        public void FileService_CheckFileExtension_ReturnsExtension_ForJpgFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("test.jpg");

            string result = _fileService.CheckFileExtension(imageFile);

            Assert.Equal(".jpg", result);
        }

        [Fact]
        public void FileService_CheckFileExtension_ReturnsExtension_ForJpegFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("photo.jpeg");

            string result = _fileService.CheckFileExtension(imageFile);

            Assert.Equal(".jpeg", result);
        }

        [Fact]
        public void FileService_CheckFileExtension_ReturnsExtension_ForPngFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("image.png");

            string result = _fileService.CheckFileExtension(imageFile);

            Assert.Equal(".png", result);
        }

        [Fact]
        public void FileService_CheckFileExtension_ReturnsExtension_ForGifFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("animation.gif");

            string result = _fileService.CheckFileExtension(imageFile);

            Assert.Equal(".gif", result);
        }

        [Fact]
        public void FileService_CheckFileExtension_IsCaseInsensitive()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("test.JPG");

            string result = _fileService.CheckFileExtension(imageFile);

            Assert.Equal(".jpg", result);
        }

        [Fact]
        public void FileService_CheckFileExtension_ThrowsException_ForUnsupportedExtension()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("document.pdf");

            Assert.Throws<UnsupportedFileExtensionException>(() => 
                _fileService.CheckFileExtension(imageFile));
        }

        [Fact]
        public void FileService_CheckFileExtension_ThrowsException_ForTxtFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("file.txt");

            Assert.Throws<UnsupportedFileExtensionException>(() => 
                _fileService.CheckFileExtension(imageFile));
        }

        [Fact]
        public void FileService_CheckFileExtension_ThrowsException_ForEmptyExtension()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("filename");

            Assert.Throws<UnsupportedFileExtensionException>(() => 
                _fileService.CheckFileExtension(imageFile));
        }

        [Fact]
        public void FileService_CheckFileExtension_ThrowsException_WithCorrectMessage()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.FileName).Returns("file.exe");

            UnsupportedFileExtensionException exception = Assert.Throws<UnsupportedFileExtensionException>(() => 
                _fileService.CheckFileExtension(imageFile));

            Assert.Equal("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.", exception.Message);
        }

        #endregion

        #region CreateDirectoryIfNotExists Tests

        [Fact]
        public void FileService_CreateDirectoryIfNotExists_CreatesDirectory_WhenNotExists()
        {
            string expectedPath = Path.Combine(_testContentRoot, "Uploads");

            if (Directory.Exists(expectedPath))
            {
                Directory.Delete(expectedPath, true);
            }

            string result = _fileService.CreateDirectoryIfNotExists();

            Assert.Equal(expectedPath, result);
            Assert.True(Directory.Exists(expectedPath));

            if (Directory.Exists(expectedPath))
            {
                Directory.Delete(expectedPath, true);
            }
        }

        [Fact]
        public void FileService_CreateDirectoryIfNotExists_ReturnsPath_WhenDirectoryExists()
        {
            string expectedPath = Path.Combine(_testContentRoot, "Uploads");
            Directory.CreateDirectory(expectedPath);

            string result = _fileService.CreateDirectoryIfNotExists();

            Assert.Equal(expectedPath, result);
            Assert.True(Directory.Exists(expectedPath));

            if (Directory.Exists(expectedPath))
            {
                Directory.Delete(expectedPath, true);
            }
        }

        [Fact]
        public void FileService_CreateDirectoryIfNotExists_UsesContentRootPath()
        {
            string result = _fileService.CreateDirectoryIfNotExists();

            Assert.StartsWith(_testContentRoot, result);

            if (Directory.Exists(result))
            {
                Directory.Delete(result, true);
            }
        }

        #endregion

        #region GetFullPathOfProfilePicture Tests

        [Fact]
        public void FileService_GetFullPathOfProfilePicture_ReturnsCorrectPath()
        {
            string fileName = "profile.jpg";
            string expectedPath = Path.Combine(_testContentRoot, "Uploads", fileName);

            string result = _fileService.GetFullPathOfProfilePicture(fileName);

            Assert.Equal(expectedPath, result);
        }

        [Fact]
        public void FileService_GetFullPathOfProfilePicture_CombinesPathsCorrectly()
        {
            string fileName = "user-123.png";

            string result = _fileService.GetFullPathOfProfilePicture(fileName);

            Assert.Contains("Uploads", result);
            Assert.EndsWith(fileName, result);
        }

        [Fact]
        public void FileService_GetFullPathOfProfilePicture_UsesContentRootPath()
        {
            string fileName = "test.jpg";

            string result = _fileService.GetFullPathOfProfilePicture(fileName);

            Assert.StartsWith(_testContentRoot, result);
        }

        #endregion

        #region CreateFileNameAsync Tests

        [Fact]
        public async Task FileService_CreateFileNameAsync_CreatesFileWithCorrectExtension()
        {
            string testPath = Path.Combine(_testContentRoot, "TestUploads");
            Directory.CreateDirectory(testPath);

            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => imageFile.CopyToAsync(A<Stream>._, A<CancellationToken>._))
                .Returns(Task.CompletedTask);

            string result = await _fileService.CreateFileNameAsync(imageFile, testPath, ".jpg");

            Assert.EndsWith(".jpg", result);

            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }

        [Fact]
        public async Task FileService_CreateFileNameAsync_GeneratesUniqueFileName()
        {
            string testPath = Path.Combine(_testContentRoot, "TestUploads");
            Directory.CreateDirectory(testPath);

            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.CopyToAsync(A<Stream>._, A<CancellationToken>._))
                .Returns(Task.CompletedTask);

            string result1 = await _fileService.CreateFileNameAsync(imageFile, testPath, ".jpg");
            string result2 = await _fileService.CreateFileNameAsync(imageFile, testPath, ".jpg");

            Assert.NotEqual(result1, result2);

            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }

        [Fact]
        public async Task FileService_CreateFileNameAsync_CallsCopyToAsync()
        {
            string testPath = Path.Combine(_testContentRoot, "TestUploads");
            Directory.CreateDirectory(testPath);

            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.CopyToAsync(A<Stream>._, A<CancellationToken>._))
                .Returns(Task.CompletedTask);

            await _fileService.CreateFileNameAsync(imageFile, testPath, ".png");

            A.CallTo(() => imageFile.CopyToAsync(A<Stream>._, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();

            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }

        [Fact]
        public async Task FileService_CreateFileNameAsync_CreatesFileInSpecifiedPath()
        {
            string testPath = Path.Combine(_testContentRoot, "TestUploads");
            Directory.CreateDirectory(testPath);

            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.CopyToAsync(A<Stream>._, A<CancellationToken>._))
                .Returns(Task.CompletedTask);

            string fileName = await _fileService.CreateFileNameAsync(imageFile, testPath, ".jpg");
            string fullPath = Path.Combine(testPath, fileName);

            Assert.True(File.Exists(fullPath));

            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
        }

        #endregion
    }
}
