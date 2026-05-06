using FakeItEasy;
using Microsoft.AspNetCore.Http;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Repositories
{
    public class RefreshTokenServiceTest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;
        private readonly IUserRepository _userRepository;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly HttpContext _httpContext;

        public RefreshTokenServiceTest()
        {
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            _refreshTokenRepository = A.Fake<IRefreshTokenRepository>();
            _jwtService = A.Fake<IJwtService>();
            _cookieService = A.Fake<ICookieService>();
            _userRepository = A.Fake<IUserRepository>();
            _httpContext = new DefaultHttpContext();

            A.CallTo(() => _httpContextAccessor.HttpContext).Returns(_httpContext);

            _refreshTokenService = new RefreshTokenService(
                _httpContextAccessor,
                _refreshTokenRepository,
                _jwtService,
                _cookieService,
                _userRepository
            );
        }

        #region GenerateNewRefreshTokenAsync Tests

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_ReturnsValidResponse()
        {
            string oldRefreshToken = "old-refresh-token";
            string newRefreshToken = "new-refresh-token";
            string newAccessToken = "new-access-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO
            {
                ID = userId,
                Username = "JohnDoe",
                Email = "johndoe@gmail.com"
            };

            RefreshToken newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns(newRefreshToken);
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns(newAccessToken);
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(newRefreshTokenEntity);
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            RefreshTokenResponseDTO result = await _refreshTokenService.GenerateNewRefreshTokenAsync();

            Assert.NotNull(result);
            Assert.Equal(newAccessToken, result.NewAccessToken);
            Assert.Equal(newRefreshToken, result.NewRefreshToken);
            Assert.Equal("JohnDoe", result.Username);
            Assert.Equal(600, result.ExpiresIn);
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_RevokesOldToken()
        {
            string oldRefreshToken = "old-refresh-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = userId, Username = "JohnDoe" };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns("new-token");
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns("new-access");
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(new RefreshToken());
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            await _refreshTokenService.GenerateNewRefreshTokenAsync();

            Assert.True(storedToken.IsRevoked);
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_CallsJwtServiceToGenerateTokens()
        {
            string oldRefreshToken = "old-refresh-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = userId, Username = "JohnDoe" };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns("new-token");
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns("new-access");
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(new RefreshToken());
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            await _refreshTokenService.GenerateNewRefreshTokenAsync();

            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_AddsNewRefreshTokenToDatabase()
        {
            string oldRefreshToken = "old-refresh-token";
            string newRefreshToken = "new-refresh-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = userId, Username = "JohnDoe" };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns(newRefreshToken);
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns("new-access");
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(new RefreshToken());
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            await _refreshTokenService.GenerateNewRefreshTokenAsync();

            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>.That.Matches(rt =>
                rt.Token == newRefreshToken &&
                rt.UserID == userId &&
                !rt.IsRevoked
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_UpdatesCookies()
        {
            string oldRefreshToken = "old-refresh-token";
            string newRefreshToken = "new-refresh-token";
            string newAccessToken = "new-access-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = userId, Username = "JohnDoe" };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns(newRefreshToken);
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns(newAccessToken);
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(new RefreshToken());
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            await _refreshTokenService.GenerateNewRefreshTokenAsync();

            A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cookieService.CreateCookie("AccessToken", newAccessToken)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cookieService.CreateCookie("RefreshToken", newRefreshToken)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_ThrowsException_WhenTokenIsNull()
        {
            string refreshToken = "invalid-token";

            _httpContext.Request.Headers.Cookie = $"RefreshToken={refreshToken}";

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(refreshToken)).Returns((RefreshToken)null);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() =>
                _refreshTokenService.GenerateNewRefreshTokenAsync());
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_ThrowsException_WhenTokenIsExpired()
        {
            string oldRefreshToken = "expired-token";

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = 1,
                ExpiryDate = DateTime.Now.AddDays(-1),
                IsRevoked = false
            };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() =>
                _refreshTokenService.GenerateNewRefreshTokenAsync());
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_ThrowsException_WhenTokenIsRevoked()
        {
            string oldRefreshToken = "revoked-token";

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = 1,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = true
            };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() =>
                _refreshTokenService.GenerateNewRefreshTokenAsync());
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_DoesNotCreateNewToken_WhenValidationFails()
        {
            string oldRefreshToken = "expired-token";

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = 1,
                ExpiryDate = DateTime.Now.AddDays(-1),
                IsRevoked = false
            };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() =>
                _refreshTokenService.GenerateNewRefreshTokenAsync());

            A.CallTo(() => _jwtService.GenerateRefreshToken(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _jwtService.GenerateAcessToken(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RefreshTokenService_GenerateNewRefreshTokenAsync_ExecutesInCorrectOrder()
        {
            string oldRefreshToken = "old-refresh-token";
            string newRefreshToken = "new-refresh-token";
            string newAccessToken = "new-access-token";
            int userId = 1;

            _httpContext.Request.Headers.Cookie = $"RefreshToken={oldRefreshToken}";

            RefreshToken storedToken = new RefreshToken
            {
                Token = oldRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = userId, Username = "JohnDoe" };
            RefreshToken newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserID = userId,
                ExpiryDate = DateTime.Now.AddDays(7)
            };

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).Returns(storedToken);
            A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).Returns(newRefreshToken);
            A.CallTo(() => _jwtService.GenerateAcessToken(userId)).Returns(newAccessToken);
            A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).Returns(newRefreshTokenEntity);
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(currentUser);

            await _refreshTokenService.GenerateNewRefreshTokenAsync();

            A.CallTo(() => _refreshTokenRepository.GetRefreshTokenAsync(oldRefreshToken)).MustHaveHappened()
                .Then(A.CallTo(() => _jwtService.GenerateRefreshToken(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _jwtService.GenerateAcessToken(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _refreshTokenRepository.AddRefreshTokenAsync(A<RefreshToken>._)).MustHaveHappened())
                .Then(A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).MustHaveHappened())
                .Then(A.CallTo(() => _cookieService.DeleteCookie("RefreshToken")).MustHaveHappened())
                .Then(A.CallTo(() => _cookieService.CreateCookie("AccessToken", newAccessToken)).MustHaveHappened())
                .Then(A.CallTo(() => _cookieService.CreateCookie("RefreshToken", newRefreshToken)).MustHaveHappened());
        }

        #endregion
    }
}
