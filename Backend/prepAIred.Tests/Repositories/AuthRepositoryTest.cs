using FakeItEasy;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class AuthRepositoryTest
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthRepository _authRepository;

        public AuthRepositoryTest()
        {
            _authService = A.Fake<IAuthService>();
            _userService = A.Fake<IUserService>();

            _authRepository = new AuthRepository(_authService, _userService);
        }

        #region Register Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_CallsValidateUser()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO();

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_CallsHashPassword()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO();

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _userService.HashPassword(userCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_CallsAuthServiceRegister_WithCorrectParameters()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO();

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_CallsGenerateAuthResponse()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_ThrowsException_WhenValidationFails()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "invalid-email",
                Password = "weak"
            };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Invalid credentials"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                _authRepository.RegisterAsync(userCredentials));

            A.CallTo(() => _userService.HashPassword(A<UserCredentialsDTO>._)).MustNotHaveHappened();
            A.CallTo(() => _authService.RegisterAsync(A<UserCredentialsDTO>._, A<byte[]>._, A<byte[]>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_ExecutesInCorrectOrder()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO();

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).MustHaveHappened()
                .Then(A.CallTo(() => _userService.HashPassword(userCredentials)).MustHaveHappened())
                .Then(A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword)).MustHaveHappened())
                .Then(A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).MustHaveHappened());
        }

        #endregion

        #region Login Tests

        [Fact]
        public async Task AuthRepository_LoginAsync_CallsAuthServiceLogin()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.LoginAsync(loginDto);

            A.CallTo(() => _authService.LoginAsync(loginDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_CallsGenerateAuthResponse()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.LoginAsync(loginDto);

            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_ThrowsException_WhenAuthenticationFails()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "WrongPassword"
            };

            A.CallTo(() => _authService.LoginAsync(loginDto))
                .ThrowsAsync(new InvalidCredentialsException("Invalid credentials"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                _authRepository.LoginAsync(loginDto));

            A.CallTo(() => _authService.GenerateAuthResponseAsync(A<CurrentUserDTO>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_ExecutesInCorrectOrder()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.LoginAsync(loginDto);

            A.CallTo(() => _authService.LoginAsync(loginDto)).MustHaveHappened()
                .Then(A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).MustHaveHappened());
        }

        #endregion

        #region Logout Tests

        [Fact]
        public async Task AuthRepository_LogoutAsync_CallsAuthServiceLogout()
        {
            A.CallTo(() => _authService.LogoutAsync()).Returns(Task.CompletedTask);

            await _authRepository.LogoutAsync();

            A.CallTo(() => _authService.LogoutAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_LogoutAsync_CompletesSuccessfully()
        {
            A.CallTo(() => _authService.LogoutAsync()).Returns(Task.CompletedTask);

            Task act() => _authRepository.LogoutAsync();

            await act();
        }

        [Fact]
        public async Task AuthRepository_LogoutAsync_ThrowsException_WhenAuthServiceFails()
        {
            A.CallTo(() => _authService.LogoutAsync())
                .ThrowsAsync(new InvalidOperationException("Logout failed"));

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _authRepository.LogoutAsync());
        }

        #endregion
    }
}
