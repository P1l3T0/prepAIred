using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prepAIred.API;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _userController = new UserController(_userService);
        }

        #region GetCurrentUser Tests

        [Fact]
        public async Task UserController_GetCurrentUser_ReturnsNoContent_WhenAccessTokenIsNull()
        {
            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            IActionResult result = await _userController.GetCurrentUser();

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UserController_GetCurrentUser_ReturnsNoContent_WhenAccessTokenIsEmpty()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Cookie = "AccessToken=";

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            IActionResult result = await _userController.GetCurrentUser();

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UserController_GetCurrentUser_ReturnsOk_WhenAccessTokenExists()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Cookie = "AccessToken=valid-token";

            CurrentUserDTO currentUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(currentUser);

            IActionResult result = await _userController.GetCurrentUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserController_GetCurrentUser_ReturnsCurrentUserDto_WhenAccessTokenExists()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Cookie = "AccessToken=valid-token";

            CurrentUserDTO currentUser = new CurrentUserDTO
            {
                ID = 1,
                Username = "JohnDoe",
                Email = "john@example.com"
            };

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(currentUser);

            IActionResult result = await _userController.GetCurrentUser();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            CurrentUserDTO returnedUser = Assert.IsType<CurrentUserDTO>(okResult.Value);
            Assert.Equal(currentUser.ID, returnedUser.ID);
            Assert.Equal(currentUser.Username, returnedUser.Username);
            Assert.Equal(currentUser.Email, returnedUser.Email);
        }

        [Fact]
        public async Task UserController_GetCurrentUser_CallsRepositoryMethod_WhenAccessTokenExists()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Cookie = "AccessToken=valid-token";

            CurrentUserDTO currentUser = new CurrentUserDTO { ID = 1, Username = "JohnDoe" };

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            A.CallTo(() => _userService.GetCurrentUserAsync()).Returns(currentUser);

            await _userController.GetCurrentUser();

            A.CallTo(() => _userService.GetCurrentUserAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserController_GetCurrentUser_DoesNotCallRepository_WhenAccessTokenIsNull()
        {
            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            await _userController.GetCurrentUser();

            A.CallTo(() => _userService.GetCurrentUserAsync()).MustNotHaveHappened();
        }

        #endregion

        #region UpdateCurrentUser Tests

        [Fact]
        public async Task UserController_UpdateCurrentUser_ReturnsOk()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.UpdateCurrentUserAsync(userCredentialsDto)).Returns(Task.CompletedTask);

            IActionResult result = await _userController.UpdateCurrentUser(userCredentialsDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserController_UpdateCurrentUser_ReturnsSuccessMessage()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.UpdateCurrentUserAsync(userCredentialsDto)).Returns(Task.CompletedTask);

            IActionResult result = await _userController.UpdateCurrentUser(userCredentialsDto);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User updated", okResult.Value);
        }

        [Fact]
        public async Task UserController_UpdateCurrentUser_CallsRepositoryMethod()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.UpdateCurrentUserAsync(userCredentialsDto)).Returns(Task.CompletedTask);

            await _userController.UpdateCurrentUser(userCredentialsDto);

            A.CallTo(() => _userService.UpdateCurrentUserAsync(userCredentialsDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UserController_UpdateCurrentUser_PassesCorrectDtoToRepository()
        {
            UserCredentialsDTO userCredentialsDto = new UserCredentialsDTO
            {
                Username = "UpdatedName",
                Email = "updated@example.com",
                Password = "NewP@ssw0rd"
            };

            A.CallTo(() => _userService.UpdateCurrentUserAsync(userCredentialsDto)).Returns(Task.CompletedTask);

            await _userController.UpdateCurrentUser(userCredentialsDto);

            A.CallTo(() => _userService.UpdateCurrentUserAsync(A<UserCredentialsDTO>.That.Matches(dto => 
                dto.Username == userCredentialsDto.Username &&
                dto.Email == userCredentialsDto.Email &&
                dto.Password == userCredentialsDto.Password
            ))).MustHaveHappenedOnceExactly();
        }

        #endregion

        #region DeleteCurrentUser Tests

        [Fact]
        public async Task UserController_DeleteCurrentUser_ReturnsOk()
        {
            A.CallTo(() => _userService.DeleteCurrentUserAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _userController.DeleteCurrentUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserController_DeleteCurrentUser_ReturnsSuccessMessage()
        {
            A.CallTo(() => _userService.DeleteCurrentUserAsync()).Returns(Task.CompletedTask);

            IActionResult result = await _userController.DeleteCurrentUser();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User deleted", okResult.Value);
        }

        [Fact]
        public async Task UserController_DeleteCurrentUser_CallsRepositoryMethod()
        {
            A.CallTo(() => _userService.DeleteCurrentUserAsync()).Returns(Task.CompletedTask);

            await _userController.DeleteCurrentUser();

            A.CallTo(() => _userService.DeleteCurrentUserAsync()).MustHaveHappenedOnceExactly();
        }

        #endregion
    }
}
