using FakeItEasy;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class UserRepositoryAdvancedTest
    {
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;
        private readonly UserRepository _userRepository;

        public UserRepositoryAdvancedTest()
        {
            _userService = A.Fake<IUserService>();
            _cookieService = A.Fake<ICookieService>();
            _userRepository = new UserRepository(_userService, _cookieService);
        }

        #region GetCurrentUserAsync Advanced Tests

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_ThrowsException_WhenUserNotFound()
        {
            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new InvalidOperationException("User not found"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.GetCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_ThrowsException_WhenTokenExpired()
        {
            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new UnauthorizedAccessException("Access token has expired"));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
                _userRepository.GetCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_ThrowsException_WhenTokenInvalid()
        {
            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new UnauthorizedAccessException("Invalid access token"));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
                _userRepository.GetCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_ReturnsUserWithAllFields()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(expectedUser);

            CurrentUserDTO result = await _userRepository.GetCurrentUserAsync();

            Assert.NotNull(result);
            Assert.Equal(expectedUser.ID, result.ID);
            Assert.Equal(expectedUser.Username, result.Username);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_HandlesDatabaseTimeout()
        {
            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new TimeoutException("Database query timed out"));

            await Assert.ThrowsAsync<TimeoutException>(() => 
                _userRepository.GetCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_HandlesDeletedUser()
        {
            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new InvalidOperationException("User account has been deleted"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.GetCurrentUserAsync());
        }

        #endregion

        #region UpdateCurrentUserAsync Advanced Tests

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ThrowsException_WhenValidationFails()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "A",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .ThrowsAsync(new InvalidCredentialsException("Username must be at least 3 characters"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));

            A.CallTo(() => _userService.GetCurrentUserID()).MustNotHaveHappened();
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _userService.UpdateUserAsync(A<User>._, A<UserCredentialsDTO>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ThrowsException_WhenEmailAlreadyTaken()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "JohnDoe",
                Email = "taken@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .ThrowsAsync(new InvalidCredentialsException("Email is already registered to another account"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ThrowsException_WhenUsernameAlreadyTaken()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "TakenUsername",
                Email = "john@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .ThrowsAsync(new InvalidCredentialsException("Username is already taken"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_RollsBackOnFailure()
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
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .ThrowsAsync(new Exception("Database transaction failed"));

            await Assert.ThrowsAsync<Exception>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));

            Assert.Equal("OldName", currentUser.Username);
            Assert.Equal("old@example.com", currentUser.Email);
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_HandlesConcurrentModification()
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
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .ThrowsAsync(new InvalidOperationException("Concurrent modification detected"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_EnsuresAtomicOperation()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            int validationCalls = 0;
            int getUserIdCalls = 0;
            int getUserEntityCalls = 0;
            int updateUserCalls = 0;

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .Invokes(() => validationCalls++)
                .Returns(Task.CompletedTask);

            A.CallTo(() => _userService.GetCurrentUserID())
                .Invokes(() =>
                {
                    Assert.Equal(1, validationCalls);
                    getUserIdCalls++;
                })
                .Returns(userId);

            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId))
                .Invokes(() =>
                {
                    Assert.Equal(1, getUserIdCalls);
                    getUserEntityCalls++;
                })
                .Returns(currentUser);

            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .Invokes(() =>
                {
                    Assert.Equal(1, getUserEntityCalls);
                    updateUserCalls++;
                })
                .Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            Assert.Equal(1, validationCalls);
            Assert.Equal(1, getUserIdCalls);
            Assert.Equal(1, getUserEntityCalls);
            Assert.Equal(1, updateUserCalls);
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_HandlesDatabaseTimeout()
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
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId))
                .ThrowsAsync(new TimeoutException("Database query timed out"));

            await Assert.ThrowsAsync<TimeoutException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ThrowsException_WhenUserNotFound()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto)).Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId))
                .ThrowsAsync(new InvalidOperationException("User not found"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_ValidatesPasswordStrengthForNewPassword()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "weak"
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .ThrowsAsync(new InvalidCredentialsException("Password does not meet complexity requirements"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        [Theory]
        [InlineData("", "updated@example.com", "NewP@ssw0rd")]
        [InlineData("UpdatedName", "", "NewP@ssw0rd")]
        [InlineData("UpdatedName", "invalid-email", "NewP@ssw0rd")]
        [InlineData("UpdatedName", "updated@example.com", "weak")]
        public async Task UserRepository_UpdateCurrentUserAsync_ValidatesAllFields(
            string username, string email, string password)
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = username,
                Email = email,
                Password = password
            };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .ThrowsAsync(new InvalidCredentialsException("Validation failed"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));
        }

        #endregion

        #region DeleteCurrentUserAsync Advanced Tests

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_DeletesCookiesEvenIfUserDeletionFails()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .ThrowsAsync(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => 
                _userRepository.DeleteCurrentUserAsync());

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_EnsuresCascadingDeletion()
        {
            int userId = 1;
            bool cascadingDeletionVerified = false;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Invokes(() => cascadingDeletionVerified = true)
                .Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            Assert.True(cascadingDeletionVerified);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_ThrowsException_WhenAlreadyDeleted()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Returns(Task.CompletedTask).Once()
                .Then.ThrowsAsync(new InvalidOperationException("User already deleted"));

            await _userRepository.DeleteCurrentUserAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.DeleteCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_HandlesActiveSessionsGracefully()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .ThrowsAsync(new InvalidOperationException("Cannot delete user with active interview sessions"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userRepository.DeleteCurrentUserAsync());
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_HandlesCookieDeletionFailure()
        {
            int userId = 1;

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken"))
                .Throws(new Exception("Cookie deletion failed"));
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

            await Assert.ThrowsAsync<Exception>(() => 
                _userRepository.DeleteCurrentUserAsync());

            A.CallTo(() => _userService.DeleteUserAsync(userId)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_DeletesBothCookiesBeforeUserDeletion()
        {
            int userId = 1;
            bool accessTokenDeleted = false;
            bool refreshTokenDeleted = false;
            bool userDeletionStarted = false;

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken"))
                .Invokes(() =>
                {
                    Assert.False(userDeletionStarted, "User deletion should not start before cookies are deleted");
                    accessTokenDeleted = true;
                });

            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken"))
                .Invokes(() =>
                {
                    Assert.True(accessTokenDeleted, "AccessToken should be deleted first");
                    Assert.False(userDeletionStarted, "User deletion should not start before cookies are deleted");
                    refreshTokenDeleted = true;
                });

            A.CallTo(() => _userService.GetCurrentUserID())
                .Invokes(() =>
                {
                    Assert.True(accessTokenDeleted, "AccessToken should be deleted before getting user ID");
                    Assert.True(refreshTokenDeleted, "RefreshToken should be deleted before getting user ID");
                })
                .Returns(userId);

            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Invokes(() =>
                {
                    Assert.True(accessTokenDeleted);
                    Assert.True(refreshTokenDeleted);
                    userDeletionStarted = true;
                })
                .Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            Assert.True(accessTokenDeleted);
            Assert.True(refreshTokenDeleted);
            Assert.True(userDeletionStarted);
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_HandlesDatabaseTimeout()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .ThrowsAsync(new TimeoutException("Database query timed out"));

            await Assert.ThrowsAsync<TimeoutException>(() => 
                _userRepository.DeleteCurrentUserAsync());
        }

        #endregion

        #region Concurrency Tests

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_HandlesConcurrentRequests()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(expectedUser);

            Task<CurrentUserDTO> task1 = _userRepository.GetCurrentUserAsync();
            Task<CurrentUserDTO> task2 = _userRepository.GetCurrentUserAsync();
            Task<CurrentUserDTO> task3 = _userRepository.GetCurrentUserAsync();

            CurrentUserDTO[] results = await Task.WhenAll(task1, task2, task3);

            Assert.All(results, user => 
            {
                Assert.Equal(expectedUser.ID, user.ID);
                Assert.Equal(expectedUser.Username, user.Username);
                Assert.Equal(expectedUser.Email, user.Email);
            });

            A.CallTo(() => _userService.GetCurrentUserAsync()).MustHaveHappened(3, Times.Exactly);
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_HandlesConcurrentUpdates()
        {
            UserCredentialsDTO userCredentialsDto1 = new UserCredentialsDTO
            {
                Username = "UpdatedName1",
                Email = "updated1@example.com",
                Password = "NewP@ssw0rd"
            };

            UserCredentialsDTO userCredentialsDto2 = new UserCredentialsDTO
            {
                Username = "UpdatedName2",
                Email = "updated2@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(A<UserCredentialsDTO>._))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto1))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto2))
                .ThrowsAsync(new InvalidOperationException("Concurrent modification detected"));

            Task task1 = _userRepository.UpdateCurrentUserAsync(userCredentialsDto1);
            Task task2 = _userRepository.UpdateCurrentUserAsync(userCredentialsDto2);

            await task1;
            await Assert.ThrowsAsync<InvalidOperationException>(() => task2);
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_HandlesConcurrentDeletionAttempts()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Returns(Task.CompletedTask).Once()
                .Then.ThrowsAsync(new InvalidOperationException("User already deleted"));

            Task task1 = _userRepository.DeleteCurrentUserAsync();
            Task task2 = _userRepository.DeleteCurrentUserAsync();

            await task1;
            await Assert.ThrowsAsync<InvalidOperationException>(() => task2);
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_PreventsRaceConditionBetweenGetAndUpdate()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId, Username = "OldName" };

            bool getUserCompleted = false;
            bool updateStarted = false;

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId))
                .Invokes(() =>
                {
                    Assert.False(updateStarted, "Update should not start before getting user entity");
                    Task.Delay(50).Wait();
                    getUserCompleted = true;
                })
                .Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .Invokes(() =>
                {
                    Assert.True(getUserCompleted, "Must get user entity before updating");
                    updateStarted = true;
                })
                .Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            Assert.True(getUserCompleted);
            Assert.True(updateStarted);
        }

        #endregion

        #region Performance Tests

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_CompletesWithinReasonableTime()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userService.GetCurrentUserAsync())
                .Returns(Task.Delay(50).ContinueWith(_ => expectedUser));

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            CurrentUserDTO result = await _userRepository.GetCurrentUserAsync();
            stopwatch.Stop();

            Assert.NotNull(result);
            Assert.True(stopwatch.ElapsedMilliseconds < 1000,
                $"GetCurrentUser took {stopwatch.ElapsedMilliseconds}ms, expected < 1000ms");
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_CompletesWithinReasonableTime()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .Returns(Task.Delay(50));
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId))
                .Returns(Task.Delay(50).ContinueWith(_ => currentUser));
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .Returns(Task.Delay(50));

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 3000,
                $"UpdateCurrentUser took {stopwatch.ElapsedMilliseconds}ms, expected < 3000ms");
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_CompletesWithinReasonableTime()
        {
            int userId = 1;

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Returns(Task.Delay(50));

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _userRepository.DeleteCurrentUserAsync();
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 2000,
                $"DeleteCurrentUser took {stopwatch.ElapsedMilliseconds}ms, expected < 2000ms");
        }

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_HandlesRapidSuccessiveUpdates()
        {
            List<UserCredentialsDTO> updateRequests = Enumerable.Range(1, 10)
                .Select(i => new UserCredentialsDTO
                {
                    Username = $"UpdatedName{i}",
                    Email = $"updated{i}@example.com",
                    Password = "NewP@ssw0rd"
                })
                .ToList();

            int userId = 1;
            User currentUser = new User { ID = userId };

            foreach (UserCredentialsDTO request in updateRequests)
            {
                A.CallTo(() => _userService.ValidateUpdateUserDataAsync(request))
                    .Returns(Task.CompletedTask);
            }

            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, A<UserCredentialsDTO>._))
                .Returns(Task.CompletedTask);

            List<Task> tasks = updateRequests
                .Select(request => _userRepository.UpdateCurrentUserAsync(request))
                .ToList();

            await Task.WhenAll(tasks);

            A.CallTo(() => _userService.UpdateUserAsync(currentUser, A<UserCredentialsDTO>._))
                .MustHaveHappened(10, Times.Exactly);
        }

        #endregion

        #region Data Integrity Tests

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_MaintainsDataConsistency()
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

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .Returns(Task.CompletedTask);

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.UpdateUserAsync(
                A<User>.That.Matches(u => u.ID == userId),
                A<UserCredentialsDTO>.That.Matches(dto =>
                    dto.Username == "UpdatedName" &&
                    dto.Email == "updated@example.com")))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserRepository_DeleteCurrentUserAsync_EnsuresCompleteCleanup()
        {
            int userId = 1;
            bool accessTokenDeleted = false;
            bool refreshTokenDeleted = false;
            bool userDeleted = false;

            A.CallTo(() => _cookieService.DeleteCookie("AccessToken"))
                .Invokes(() => accessTokenDeleted = true);
            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken"))
                .Invokes(() => refreshTokenDeleted = true);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.DeleteUserAsync(userId))
                .Invokes(() => userDeleted = true)
                .Returns(Task.CompletedTask);

            await _userRepository.DeleteCurrentUserAsync();

            Assert.True(accessTokenDeleted, "AccessToken cookie should be deleted");
            Assert.True(refreshTokenDeleted, "RefreshToken cookie should be deleted");
            Assert.True(userDeleted, "User should be deleted from database");
        }

        #endregion

        #region Error Recovery Tests

        [Fact]
        public async Task UserRepository_UpdateCurrentUserAsync_RecoverFromTransientError()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            int userId = 1;
            User currentUser = new User { ID = userId };

            A.CallTo(() => _userService.ValidateUpdateUserDataAsync(userCredentialsDto))
                .Returns(Task.CompletedTask);
            A.CallTo(() => _userService.GetCurrentUserID()).Returns(userId);
            A.CallTo(() => _userService.GetCurrentUserEntityByIdAsync(userId)).Returns(currentUser);
            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .ThrowsAsync(new Exception("Transient database error")).Once()
                .Then.Returns(Task.CompletedTask);

            await Assert.ThrowsAsync<Exception>(() => 
                _userRepository.UpdateCurrentUserAsync(userCredentialsDto));

            await _userRepository.UpdateCurrentUserAsync(userCredentialsDto);

            A.CallTo(() => _userService.UpdateUserAsync(currentUser, userCredentialsDto))
                .MustHaveHappened(2, Times.Exactly);
        }

        [Fact]
        public async Task UserRepository_GetCurrentUserAsync_RecoverFromNetworkError()
        {
            CurrentUserDTO expectedUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            A.CallTo(() => _userService.GetCurrentUserAsync())
                .ThrowsAsync(new Exception("Network connection lost")).Once()
                .Then.Returns(expectedUser);

            await Assert.ThrowsAsync<Exception>(() => _userRepository.GetCurrentUserAsync());

            CurrentUserDTO result = await _userRepository.GetCurrentUserAsync();

            Assert.NotNull(result);
            Assert.Equal(expectedUser.ID, result.ID);
        }

        #endregion
    }
}
