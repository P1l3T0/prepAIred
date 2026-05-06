using FakeItEasy;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Services
{
    public class UserServiceTest
    {
        private readonly IUserRepository _userRepository;
        private readonly ICookieService _cookieService;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _cookieService = A.Fake<ICookieService>();

            _userService = new UserService(_userRepository, _cookieService);
        }

        #region GetCurrentUserAsync Tests

        [Fact]
        public async Task UserService_GetCurrentUserAsync_ReturnsCurrentUser()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userRepository.GetCurrentUserAsync()).Returns(expectedUser);

            CurrentUserDTO result = await _userService.GetCurrentUserAsync();

            Assert.Equal(expectedUser.ID, result.ID);
            Assert.Equal(expectedUser.Username, result.Username);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task UserService_GetCurrentUserAsync_CallsUserService()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _userRepository.GetCurrentUserAsync()).Returns(expectedUser);

            await _userService.GetCurrentUserAsync();

            A.CallTo(() => _userRepository.GetCurrentUserAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region UpdateCurrentUserAsync Tests

        [Fact]
        public async Task UserService_UpdateCurrentUserAsync_ValidatesUserData()
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

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userService.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_UpdateCurrentUserAsync_GetsCurrentUserID()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userService.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_UpdateCurrentUserAsync_GetsCurrentUserEntity()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userService.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_UpdateCurrentUserAsync_CallsUpdateUserAsync()
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

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userService.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_UpdateCurrentUserAsync_ExecutesInCorrectOrder()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).Returns(Task.CompletedTask);

            await _userService.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto)).MustHaveHappened()
                .Then(A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.GetCurrentUserEntityByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.UpdateUserAsync(currentUser, userCredentialsDto)).MustHaveHappened());
        }

        #endregion

        #region DeleteCurrentUserAsync Tests

        [Fact]
        public async Task UserService_DeleteCurrentUserAsync_DeletesAccessTokenCookie()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_DeleteCurrentUserAsync_DeletesRefreshTokenCookie()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_DeleteCurrentUserAsync_GetsCurrentUserID()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteCurrentUserAsync();

            A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_DeleteCurrentUserAsync_DeletesUser()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteCurrentUserAsync();

            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserService_DeleteCurrentUserAsync_ExecutesInCorrectOrder()
        {
            int userId = 1;

            A.CallTo(() => _userRepository.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userRepository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteCurrentUserAsync();

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken")).MustHaveHappened()
                .Then(A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.GetCurrentUserID()).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.DeleteUserAsync(userId)).MustHaveHappened());
        }

        #endregion
    }
}
