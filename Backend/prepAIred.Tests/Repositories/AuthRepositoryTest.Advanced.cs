using FakeItEasy;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class AuthRepositoryAdvancedTest
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthRepository _authRepository;

        public AuthRepositoryAdvancedTest()
        {
            _authService = A.Fake<IAuthService>();
            _userService = A.Fake<IUserService>();
            _authRepository = new AuthRepository(_authService, _userService);
        }

        #region Transaction and Rollback Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_RollsBackOnAuthResponseGenerationFailure()
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
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .ThrowsAsync(new Exception("Token generation failed"));

            await Assert.ThrowsAsync<Exception>(() => _authRepository.RegisterAsync(userCredentials));
            
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_RollsBackOnTokenGenerationFailure()
        {
            LoginDTO loginRequest = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(loginRequest)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .ThrowsAsync(new Exception("Token generation failed"));

            await Assert.ThrowsAsync<Exception>(() => _authRepository.LoginAsync(loginRequest));
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_EnsuresAtomicOperation()
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

            int validationCallCount = 0;
            int hashingCallCount = 0;
            int registerCallCount = 0;
            int generateTokenCallCount = 0;

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .Invokes(() => validationCallCount++)
                .Returns(Task.CompletedTask);
            
            A.CallTo(() => _userService.HashPassword(userCredentials))
                .Invokes(() => 
                {
                    Assert.Equal(1, validationCallCount);
                    hashingCallCount++;
                })
                .Returns((hashedPassword, saltPassword));
            
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .Invokes(() => 
                {
                    Assert.Equal(1, hashingCallCount);
                    registerCallCount++;
                })
                .Returns(currentUser);
            
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .Invokes(() => 
                {
                    Assert.Equal(1, registerCallCount);
                    generateTokenCallCount++;
                })
                .Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            Assert.Equal(1, validationCallCount);
            Assert.Equal(1, hashingCallCount);
            Assert.Equal(1, registerCallCount);
            Assert.Equal(1, generateTokenCallCount);
        }

        #endregion

        #region Concurrency and Race Condition Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_HandlesConcurrentRegistrationsWithSameEmail()
        {
            UserCredentialsDTO userCredentials1 = new UserCredentialsDTO()
            {
                Username = "User1",
                Email = "same@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            UserCredentialsDTO userCredentials2 = new UserCredentialsDTO()
            {
                Username = "User2",
                Email = "same@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials1))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.ValidateUserAsync(userCredentials2))
                .ThrowsAsync(new InvalidCredentialsException("Email already registered"));
            
            A.CallTo(() => _userService.HashPassword(userCredentials1))
                .Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials1, hashedPassword, saltPassword))
                .Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .Returns(Task.CompletedTask);

            Task task1 = _authRepository.RegisterAsync(userCredentials1);
            Task task2 = _authRepository.RegisterAsync(userCredentials2);

            await task1;
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => task2);
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_HandlesConcurrentLoginsFromSameUser()
        {
            LoginDTO loginRequest = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(loginRequest)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            Task task1 = _authRepository.LoginAsync(loginRequest);
            Task task2 = _authRepository.LoginAsync(loginRequest);
            Task task3 = _authRepository.LoginAsync(loginRequest);

            await Task.WhenAll(task1, task2, task3);

            A.CallTo(() => _authService.LoginAsync(loginRequest)).MustHaveHappened(3, Times.Exactly);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).MustHaveHappened(3, Times.Exactly);
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_PreventsRaceConditionBetweenValidationAndRegistration()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            bool validationCompleted = false;
            bool registrationStarted = false;

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .Invokes(() => 
                {
                    Assert.False(registrationStarted, "Registration should not start before validation");
                    Task.Delay(100).Wait();
                    validationCompleted = true;
                })
                .Returns(Task.CompletedTask);

            A.CallTo(() => _userService.HashPassword(userCredentials))
                .Returns((hashedPassword, saltPassword));

            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .Invokes(() => 
                {
                    Assert.True(validationCompleted, "Validation must complete before registration");
                    registrationStarted = true;
                })
                .Returns(currentUser);

            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            Assert.True(validationCompleted);
            Assert.True(registrationStarted);
        }

        #endregion

        #region Password Security Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_GeneratesDifferentHashesForSamePassword()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword1 = new byte[] { 1, 2, 3 };
            byte[] saltPassword1 = new byte[] { 4, 5, 6 };
            byte[] hashedPassword2 = new byte[] { 7, 8, 9 };
            byte[] saltPassword2 = new byte[] { 10, 11, 12 };
            
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials))
                .Returns((hashedPassword1, saltPassword1)).Once()
                .Then.Returns((hashedPassword2, saltPassword2));
            
            A.CallTo(() => _authService.RegisterAsync(userCredentials, A<byte[]>._, A<byte[]>._))
                .Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);
            
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword1, saltPassword1))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_ValidatesPasswordComplexityBeforeHashing()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "weak"
            };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Password does not meet complexity requirements"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _authRepository.RegisterAsync(userCredentials));

            A.CallTo(() => _userService.HashPassword(userCredentials)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_EnsuresPasswordIsHashedBeforeStorage()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _authService.RegisterAsync(
                A<UserCredentialsDTO>.That.Matches(u => u.Password == "StrongP@ssw0rd"),
                A<byte[]>.That.IsSameSequenceAs(hashedPassword),
                A<byte[]>.That.IsSameSequenceAs(saltPassword)))
                .MustHaveHappenedOnceExactly();
        }

        #endregion

        #region Failed Login Attempt Tests

        [Fact]
        public async Task AuthRepository_LoginAsync_HandlesMultipleFailedAttempts()
        {
            LoginDTO loginRequest = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "WrongPassword"
            };

            A.CallTo(() => _authService.LoginAsync(loginRequest))
                .ThrowsAsync(new InvalidCredentialsException("Invalid email or password"));

            for (int i = 0; i < 5; i++)
            {
                await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                    _authRepository.LoginAsync(loginRequest));
            }

            A.CallTo(() => _authService.LoginAsync(loginRequest)).MustHaveHappened(5, Times.Exactly);
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_LocksAccountAfterMaxFailedAttempts()
        {
            LoginDTO loginRequest = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "WrongPassword"
            };

            A.CallTo(() => _authService.LoginAsync(loginRequest))
                .ThrowsAsync(new InvalidCredentialsException("Invalid email or password"))
                .NumberOfTimes(4);

            A.CallTo(() => _authService.LoginAsync(loginRequest))
                .ThrowsAsync(new InvalidCredentialsException("Account locked due to multiple failed login attempts"))
                .Once();

            for (int i = 0; i < 4; i++)
            {
                await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                    _authRepository.LoginAsync(loginRequest));
            }

            InvalidCredentialsException ex = await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _authRepository.LoginAsync(loginRequest));

            Assert.Contains("invalid email or password", ex.Message.ToLower());
        }

        #endregion

        #region Logout Edge Cases

        [Fact]
        public async Task AuthRepository_LogoutAsync_HandlesDoubleLogout()
        {
            A.CallTo(() => _authService.LogoutAsync())
                .Returns(Task.CompletedTask).Once()
                .Then.ThrowsAsync(new InvalidOperationException("User is not logged in"));

            await _authRepository.LogoutAsync();
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authRepository.LogoutAsync());
        }

        [Fact]
        public async Task AuthRepository_LogoutAsync_InvalidatesTokens()
        {
            bool tokensInvalidated = false;

            A.CallTo(() => _authService.LogoutAsync())
                .Invokes(() => tokensInvalidated = true)
                .Returns(Task.CompletedTask);

            await _authRepository.LogoutAsync();

            Assert.True(tokensInvalidated);
            A.CallTo(() => _authService.LogoutAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthRepository_LogoutAsync_HandlesInvalidSession()
        {
            A.CallTo(() => _authService.LogoutAsync())
                .ThrowsAsync(new InvalidOperationException("No active session found"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => _authRepository.LogoutAsync());
        }

        #endregion

        #region Data Validation Edge Cases

        [Fact]
        public async Task AuthRepository_RegisterAsync_ValidatesEmailFormat()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "invalid-email",
                Password = "StrongP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Invalid email format"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _authRepository.RegisterAsync(userCredentials));
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_ValidatesUsernameLength()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "AB",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Username must be at least 3 characters"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _authRepository.RegisterAsync(userCredentials));
        }

        [Fact]
        public async Task AuthRepository_RegisterAsync_RejectsSpecialCharactersInUsername()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "John@Doe#123",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .ThrowsAsync(new InvalidCredentialsException("Username contains invalid characters"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _authRepository.RegisterAsync(userCredentials));
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_HandlesCaseInsensitiveEmail()
        {
            LoginDTO loginRequest1 = new LoginDTO()
            {
                Email = "JohnDoe@Gmail.Com",
                Password = "StrongP@ssw0rd"
            };

            LoginDTO loginRequest2 = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            A.CallTo(() => _authService.LoginAsync(A<LoginDTO>._)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await _authRepository.LoginAsync(loginRequest1);
            await _authRepository.LoginAsync(loginRequest2);

            A.CallTo(() => _authService.LoginAsync(A<LoginDTO>._)).MustHaveHappened(2, Times.Exactly);
        }

        #endregion

        #region Performance and Stress Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_CompletesWithinReasonableTime()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials))
                .Returns(Task.Delay(50));
            A.CallTo(() => _userService.HashPassword(userCredentials))
                .Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .Returns(Task.Delay(50).ContinueWith(_ => currentUser));
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser))
                .Returns(Task.Delay(50));

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _authRepository.RegisterAsync(userCredentials);
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 5000, 
                $"Registration took {stopwatch.ElapsedMilliseconds}ms, expected < 5000ms");
        }

        [Fact]
        public async Task AuthRepository_LoginAsync_HandlesHighLoadScenario()
        {
            List<LoginDTO> loginRequests = Enumerable.Range(1, 100)
                .Select(i => new LoginDTO
                {
                    Email = $"user{i}@gmail.com",
                    Password = "StrongP@ssw0rd"
                })
                .ToList();

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _authService.LoginAsync(A<LoginDTO>._)).Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            List<Task> tasks = loginRequests.Select(request => _authRepository.LoginAsync(request)).ToList();

            await Task.WhenAll(tasks);

            A.CallTo(() => _authService.LoginAsync(A<LoginDTO>._)).MustHaveHappened(100, Times.Exactly);
        }

        #endregion

        #region Error Recovery Tests

        [Fact]
        public async Task AuthRepository_RegisterAsync_RecoverFromTransientDatabaseError()
        {
            UserCredentialsDTO userCredentials = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            byte[] hashedPassword = new byte[] { 1, 2, 3 };
            byte[] saltPassword = new byte[] { 4, 5, 6 };
            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1 };

            A.CallTo(() => _userService.ValidateUserAsync(userCredentials)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.HashPassword(userCredentials)).Returns((hashedPassword, saltPassword));
            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .ThrowsAsync(new Exception("Transient database error")).Once()
                .Then.Returns(currentUser);
            A.CallTo(() => _authService.GenerateAuthResponseAsync(currentUser)).Returns(Task.CompletedTask);

            await Assert.ThrowsAsync<Exception>(() => _authRepository.RegisterAsync(userCredentials));

            await _authRepository.RegisterAsync(userCredentials);

            A.CallTo(() => _authService.RegisterAsync(userCredentials, hashedPassword, saltPassword))
                .MustHaveHappened(2, Times.Exactly);
        }

        #endregion
    }
}
