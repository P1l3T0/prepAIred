using FakeItEasy;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class AuthServiceTest
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _authRepository = A.Fake<IAuthRepository>();
            _userRepository = A.Fake<IUserRepository>();

            _authService = new AuthService(_authRepository, _userRepository);
        }

        #region Register Tests

        [Fact]
        public async Task AuthService_RegisterAsync_CallsValidateUser()
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

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.RegisterAsync(userCredentials);

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_RegisterAsync_CallsHashPassword()
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

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.RegisterAsync(userCredentials);

            A.CallTo(() => _userRepository.HashPassword(userCredentials)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_RegisterAsync_CallsAuthServiceRegister_WithCorrectParameters()
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

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.RegisterAsync(userCredentials);

            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_RegisterAsync_CallsGenerateAuthResponse()
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

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.RegisterAsync(userCredentials);

            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_RegisterAsync_ThrowsException_WhenValidationFails()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "invalid-email",
                Password = "weak"
            };

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Invalid credentials"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                _authService.RegisterAsync(userCredentials));

            A.CallTo(() => _userRepository.HashPassword(A<UserCredentialsDTO>._)).MustNotHaveHappened();
            A.CallTo(() => _authRepository.RegisterAsync(A<UserCredentialsDTO>._, A<byte[]>._, A<byte[]>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AuthService_RegisterAsync_ExecutesInCorrectOrder()
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

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userRepository.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.RegisterAsync(userCredentials);

            A.CallTo(() => _userRepository.ValidateUserAsync(userCredentials)).MustHaveHappened()
                .Then(A.CallTo(() => _userRepository.HashPassword(userCredentials)).MustHaveHappened())
                .Then(A.CallTo(() => _authRepository.RegisterAsync(userCredentials, hashedPassword, saltPassword)).MustHaveHappened())
                .Then(A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).MustHaveHappened());
        }

        #endregion

        #region Login Tests

        [Fact]
        public async Task AuthService_LoginAsync_CallsAuthServiceLogin()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authRepository.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.LoginAsync(loginDto);

            A.CallTo(() => _authRepository.LoginAsync(loginDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_LoginAsync_CallsGenerateAuthResponse()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authRepository.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.LoginAsync(loginDto);

            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_LoginAsync_ThrowsException_WhenAuthenticationFails()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "WrongPassword"
            };

            A.CallTo(() => _authRepository.LoginAsync(loginDto))
                .ThrowsAsync(new InvalidCredentialsException("Invalid credentials"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                _authService.LoginAsync(loginDto));

            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(A<CurrentUserDTO>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AuthService_LoginAsync_ExecutesInCorrectOrder()
        {
            LoginDTO loginDto = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authRepository.LoginAsync(loginDto)).Returns(currentUser);
            A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authService.LoginAsync(loginDto);

            A.CallTo(() => _authRepository.LoginAsync(loginDto)).MustHaveHappened()
                .Then(A.CallTo(() => _authRepository.GenerateAuthResponseAsync(currentUser)).MustHaveHappened());
        }

        #endregion

        #region Logout Tests

        [Fact]
        public async Task AuthService_LogoutAsync_CallsAuthServiceLogout()
        {
            A.CallTo(() => _authRepository.LogoutAsync()).Returns(Task.CompletedTask);

            await _authService.LogoutAsync();

            A.CallTo(() => _authRepository.LogoutAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthService_LogoutAsync_CompletesSuccessfully()
        {
            A.CallTo(() => _authRepository.LogoutAsync()).Returns(Task.CompletedTask);

            Task act() => _authService.LogoutAsync();

            await act();
        }

        [Fact]
        public async Task AuthService_LogoutAsync_ThrowsException_WhenAuthServiceFails()
        {
            A.CallTo(() => _authRepository.LogoutAsync())
                .ThrowsAsync(new InvalidOperationException("Logout failed"));

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _authService.LogoutAsync());
        }

        #endregion
    }
}
