using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using prepAIred.API;
using prepAIred.Data;
using prepAIred.Services;

namespace prepAIred.Tests.Controllers
{
    public class ProfilePictureControllerTest
    {
        private readonly IProfilePictureRepository _profilePictureRepository;
        private readonly ProfilePictureController _profilePictureController;

        public ProfilePictureControllerTest()
        {
            _profilePictureRepository = A.Fake<IProfilePictureRepository>();
            _profilePictureController = new ProfilePictureController(_profilePictureRepository);
        }

        #region GetProfilePicture Tests

        [Fact]
        public async Task ProfilePictureController_GetProfilePicture_ReturnsOk()
        {
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlAsync()).Returns(expectedUrl);

            IActionResult result = await _profilePictureController.GetProfilePicture();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ProfilePictureController_GetProfilePicture_ReturnsCorrectUrl()
        {
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlAsync()).Returns(expectedUrl);

            IActionResult result = await _profilePictureController.GetProfilePicture();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedUrl, okResult.Value);
        }

        [Fact]
        public async Task ProfilePictureController_GetProfilePicture_CallsRepositoryMethod()
        {
            string expectedUrl = "https://localhost:7227/Uploads/profile.jpg";

            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlAsync()).Returns(expectedUrl);

            await _profilePictureController.GetProfilePicture();

            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureController_GetProfilePicture_ReturnsEmptyString_WhenNoProfilePicture()
        {
            A.CallTo(() => _profilePictureRepository.GetProfilePictureUrlAsync()).Returns(string.Empty);

            IActionResult result = await _profilePictureController.GetProfilePicture();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(string.Empty, okResult.Value);
        }

        #endregion

        #region ChangeProfilePicture Tests

        [Fact]
        public async Task ProfilePictureController_ChangeProfilePicture_ReturnsOk()
        {
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO();

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto)).Returns(Task.CompletedTask);

            IActionResult result = await _profilePictureController.ChangeProfilePicture(profilePictureDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ProfilePictureController_ChangeProfilePicture_ReturnsSuccessMessage()
        {
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO();

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto)).Returns(Task.CompletedTask);

            IActionResult result = await _profilePictureController.ChangeProfilePicture(profilePictureDto);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Profile picture changed", okResult.Value);
        }

        [Fact]
        public async Task ProfilePictureController_ChangeProfilePicture_CallsRepositoryMethod()
        {
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO();

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto)).Returns(Task.CompletedTask);

            await _profilePictureController.ChangeProfilePicture(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProfilePictureController_ChangeProfilePicture_PassesCorrectDtoToRepository()
        {
            ProfilePictureDTO profilePictureDto = new ProfilePictureDTO();

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(profilePictureDto)).Returns(Task.CompletedTask);

            await _profilePictureController.ChangeProfilePicture(profilePictureDto);

            A.CallTo(() => _profilePictureRepository.ChangeProfilePictureAsync(A<ProfilePictureDTO>.That.Matches(dto => dto == profilePictureDto)))
                .MustHaveHappenedOnceExactly();
        }

        #endregion
    }
}
