using FakeItEasy;
using Microsoft.AspNetCore.Http;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class ProfilePictureServiceTest
    {
        private readonly IProfilePictureRepository _profilePictureRepository;
        private readonly IUserRepository _userRepository;
        private readonly ProfilePictureService _profilePictureService;

        public ProfilePictureServiceTest()
        {
            _profilePictureRepository = A.Fake<IProfilePictureRepository>();
            _userRepository = A.Fake<IUserRepository>();

            _profilePictureService = new ProfilePictureService(_profilePictureRepository, _userRepository);
        }

        #region GetProfilePictureUrlAsync Tests

        [Fact]
        public async Task ProfilePictureService_GetProfilePictureUrlAsync_ReturnsCorrectUrl()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            string result = await _profilePictureService.GetProfilePictureUrlAsync();

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public async Task ProfilePictureService_GetProfilePictureUrlAsync_CallsGetCurrentUserID()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureService.GetProfilePictureUrlAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_GetProfilePictureUrlAsync_CallsGetProfilePictureUrlByUserIdAsync()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureService.GetProfilePictureUrlAsync();

            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_GetProfilePictureUrlAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).Returns(expectedUrl);

            await _profilePictureService.GetProfilePictureUrlAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened()
                .Then(A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(userId)).MustHaveHappened());
        }

        #endregion

        #region ChangeProfilePictureAsync Tests

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_SavesNewFile()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(fileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_GetsCurrentUser()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(fileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_DeletesOldPicture_WhenExists()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _profilePictureRepository.DeleteProfilePictureAsync(oldFileName)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.DeleteProfilePictureAsync(oldFileName)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_DoesNotDeleteOldPicture_WhenNotExists()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.DeleteProfilePictureAsync(A<string>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_UpdatesUserProfilePicture()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            Assert.Equal(newFileName, currentUser.ProfilePicture);
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_CallsUpdateUserAsync()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_ExecutesInCorrectOrder_WithoutOldPicture()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).MustHaveHappened()
                .Then(A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).MustHaveHappened());
        }

        [Fact]
        public async Task ProfilePictureService_ChangeProfilePictureAsync_ExecutesInCorrectOrder_WithOldPicture()
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

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).Returns(newFileName);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _profilePictureRepository.DeleteProfilePictureAsync(oldFileName)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).Returns(Task.CompletedTask);

            await _profilePictureService.ChangeProfilePictureAsync(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.SaveFileAsync(imageFile)).MustHaveHappened()
                .Then(A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _profilePictureRepository.DeleteProfilePictureAsync(oldFileName)).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, null)).MustHaveHappened());
        }

        #endregion
    }
}
