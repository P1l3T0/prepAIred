using FakeItEasy;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userService = A.Fake<IUserService>();
            _cookieService = A.Fake<ICookieService>();

            _userRepository = new UserRepository(_userService, _cookieService);
        }

        #region GetCurrentUserAsync Tests

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_ReturnsCurrentUser()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(expectedUser);

            CurrentUserDTO result = await _userRepository.GetCurrentUserAsync();

            Assert.Equal(expectedUser.ID, result.ID);
            Assert.Equal(expectedUser.Username, result.Username);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_CallsUserService()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(expectedUser);

            await _userRepository.GetCurrentUserAsync();

            A.CallTo(() => _userService.GetCurrentUserAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region UpdateCurrentUserAsync Tests

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ValidatesUserData()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User
            {
                ID = userId,
                Username = "OldName",
                Email = "old@example.com"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_GetsCurrentUserID()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_GetsCurrentUserEntity()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_CallsUpdateUserAsync()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User
            {
                ID = userId,
                Username = "OldName",
                Email = "old@example.com"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ExecutesInCorrectOrder()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).MustHaveHappened()
                .Then(A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto)).MustHaveHappened());
        }

        #endregion

        #region DeleteCurrentUserAsync Tests

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_DeletesAccessTokenCookie()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_DeletesRefreshTokenCookie()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_GetsCurrentUserID()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_DeletesUser()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            A.CallTo(() => _userService.DeleteUserAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken")).MustHaveHappened()
                .Then(A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappened())
                .Then(A.CallTo(() => _userService.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userService.DeleteUserAsync(userId)).MustHaveHappened());
        }

        #endregion
    }
}
