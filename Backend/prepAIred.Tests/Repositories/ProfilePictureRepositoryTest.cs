using FakeItEasy;
using Microsoft.AspNetCore.Http;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class ProfilePictureRepositoryTest
    {
        private readonly IFileService _fileService;

        public ProfilePictureRepositoryTest()
        {
            _fileService = A.Fake<IFileService>();
        }

        #region SaveFileAsync Tests

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_ThrowsException_WhenFileIsNull()
        {
            await Assert.ThrowsAsync<ProfilePictureException>(() =>
                TestSaveFileAsync(null));
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_ThrowsException_WhenFileLengthIsZero()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.Length).Returns(0);

            await Assert.ThrowsAsync<ProfilePictureException>(() =>
                TestSaveFileAsync(imageFile));
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_ThrowsException_WithCorrectMessage()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.Length).Returns(0);

            ProfilePictureException exception = await Assert.ThrowsAsync<ProfilePictureException>(() =>
                TestSaveFileAsync(imageFile));

            Assert.Equal("File is null or empty", exception.Message);
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_CallsCreateDirectoryIfNotExists()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).Returns("/path/to/uploads");
            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).Returns(".jpg");
            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, A<string>._, A<string>._)).Returns("file.jpg");

            await TestSaveFileAsync(imageFile);

            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_CallsCheckFileExtension()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).Returns("/path");
            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).Returns(".jpg");
            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, A<string>._, A<string>._)).Returns("file.jpg");

            await TestSaveFileAsync(imageFile);

            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_CallsCreateFileNameAsync_WithCorrectParameters()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            string path = "/uploads/path";
            string extension = ".png";

            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).Returns(path);
            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).Returns(extension);
            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, path, extension)).Returns("unique-name.png");

            await TestSaveFileAsync(imageFile);

            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, path, extension)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_ReturnsFileName()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            string expectedFileName = "generated-guid.jpg";

            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).Returns("/path");
            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).Returns(".jpg");
            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, A<string>._, A<string>._)).Returns(expectedFileName);

            string result = await TestSaveFileAsync(imageFile);

            Assert.Equal(expectedFileName, result);
        }

        [Fact]
        public async Task ProfilePictureRepository_SaveFileAsync_ExecutesInCorrectOrder()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            string path = "/uploads";
            string extension = ".jpg";
            string fileName = "file.jpg";

            A.CallTo(() => imageFile.Length).Returns(100);
            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).Returns(path);
            A.CallTo(() => _fileService.CheckFileExtension(imageFile)).Returns(extension);
            A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, path, extension)).Returns(fileName);

            await TestSaveFileAsync(imageFile);

            A.CallTo(() => _fileService.CreateDirectoryIfNotExists()).MustHaveHappened()
                .Then(A.CallTo(() => _fileService.CheckFileExtension(imageFile)).MustHaveHappened())
                .Then(A.CallTo(() => _fileService.CreateFileNameAsync(imageFile, path, extension)).MustHaveHappened());
        }

        #endregion

        #region Helper Methods

        private async Task<string> TestSaveFileAsync(IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                throw new ProfilePictureException("File is null or empty");
            }

            string path = _fileService.CreateDirectoryIfNotExists();
            string extension = _fileService.CheckFileExtension(imageFile);
            string fileName = await _fileService.CreateFileNameAsync(imageFile, path, extension);

            return fileName;
        }

        #endregion
    }
}
