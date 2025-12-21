using FakeItEasy;
using Microsoft.AspNetCore.Http;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class ProfilePictureRepositoryTest
    {
        private readonly IProfilePictureService _profilePictureService;
        private readonly IUserService _userService;
        private readonly ProfilePictureRepository _profilePictureRepository;

        public ProfilePictureRepositoryTest()
        {
            _profilePictureService = A.Fake<IProfilePictureService>();
            _userService = A.Fake<IUserService>();

            _profilePictureRepository = new ProfilePictureRepository(_profilePictureService, _userService);
        }

        #region GetProfilePictureUrlAsync Tests

        [Fact]
        public async Task ProfilePictureRepository_GetProfilePictureUrlAsync_ReturnsCorrectUrl()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            string result = await _profilePictureRepository.GetProfilePictureUrlAsync();

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public async Task ProfilePictureRepository_GetProfilePictureUrlAsync_CallsGetCurrentUserID()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureRepository.GetProfilePictureUrlAsync();

            A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_GetProfilePictureUrlAsync_CallsGetProfilePictureUrlByUserIdAsync()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureRepository.GetProfilePictureUrlAsync();

            A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_GetProfilePictureUrlAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureRepository.GetProfilePictureUrlAsync();

            A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappened()
                .Then(A.CallTo(() => _profilePictureService.GetProfilePictureUrlByUserIdAsync(userId)).MustHaveHappened());
        }

        #endregion

        #region ChangeProfilePictureAsync Tests

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_SavesNewFile()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string fileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(fileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_GetsCurrentUser()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string fileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(fileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_DeletesOldPicture_WhenExists()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string oldFileName = "old-profile.jpg";
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = oldFileName
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _profilePictureService.DeleteProfilePictureAsync(oldFileName)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureService.DeleteProfilePictureAsync(oldFileName)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_DoesNotDeleteOldPicture_WhenNotExists()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureService.DeleteProfilePictureAsync(A<string>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_UpdatesUserProfilePicture()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            Assert.Equal(newFileName, currentUser.ProfilePicture);
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_CallsUpdateUserAsync()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_ExecutesInCorrectOrder_WithoutOldPicture()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = string.Empty
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).MustHaveHappened()
                .Then(A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).MustHaveHappened());
        }

        [Fact]
        public async Task ProfilePictureRepository_ChangeProfilePictureAsync_ExecutesInCorrectOrder_WithOldPicture()
        {
            IFormFile imageFile = A.Fake<IFormFile>();
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO { ImageFile = imageFile };
            string oldFileName = "old-profile.jpg";
            string newFileName = "new-profile.jpg";
            int userId = 1;

            User currentUser = new User
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "john@example.com",
                ProfilePicture = oldFileName
            };

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _profilePictureService.DeleteProfilePictureAsync(oldFileName)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureService.SaveFileAsync(imageFile)).MustHaveHappened()
                .Then(A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _profilePictureService.DeleteProfilePictureAsync(oldFileName)).MustHaveHappened())
                .Then(A.CallTo(() => _userService.UpdateUserAsync(currentUser, null)).MustHaveHappened());
        }

        #endregion
    }
}
