using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using prepAIred.API;
using prepAIred.Data;
using prepAIred.Exceptions;
using prepAIred.Services;

namespace prepAIred.Tests.Controllers
{
    public class AuthControllerTest
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AuthController _authController;

        public AuthControllerTest()
        {
            _authService = A.Fake<IAuthService>();
            _refreshTokenService = A.Fake<IRefreshTokenService>();

            _authController = new AuthController(_authService, _refreshTokenService);
        }

        [Fact]
        public async Task AuthController_Register_ReturnsOk_ForValidCredentials()
        {
            UserCredentialsDTO registerRequest = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            A.CallTo(() => _authService.RegisterAsync(registerRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _authController.Register(registerRequest);

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _authService.RegisterAsync(registerRequest)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthController_Register_ThrowsException_ForWeakPassword()
        {
            UserCredentialsDTO registerRequest = new UserCredentialsDTO()
            {
                Username = "JohnDoe",
                Email = "johndoe@gmail.com",
                Password = "weakpassword"
            };

            A.CallTo(() => _authService.RegisterAsync(registerRequest))
                .ThrowsAsync(new InvalidCredentialsException("Password does not meet complexity requirements"));

            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _authController.Register(registerRequest));
        }

        [Fact]
        public async Task AuthController_Login_ReturnsOk_ForValidCredentials()
        {
            LoginDTO loginRequest = new LoginDTO()
            {
                Email = "johndoe@gmail.com",
                Password = "StrongP@ssw0rd"
            };

            A.CallTo(() => _authService.LoginAsync(loginRequest)).Returns(Task.CompletedTask);

            IActionResult result = await _authController.Login(loginRequest);

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _authService.LoginAsync(loginRequest)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthController_Logout_ReturnsOk()
        {
            A.CallTo(() => _authService.LogoutAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _authController.Logout();

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _authService.LogoutAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthController_GenerateNewRefreshToken_ReturnsOk()
        {
            RefreshTokenResponseDTO tokenResponse = new RefreshTokenResponseDTO();
            A.CallTo(() => _refreshTokenService.GenerateNewRefreshTokenAsync()).Returns(tokenResponse);

            IActionResult result = await _authController.GenerateNewRefreshToken();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tokenResponse, okResult.Value);
        }
    }
}
