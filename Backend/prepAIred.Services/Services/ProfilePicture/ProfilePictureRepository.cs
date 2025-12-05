using prepAIred.Data;

namespace prepAIred.Services
{
    public class ProfilePictureRepository(IProfilePictureService profilePictureService, IUserService userService) : IProfilePictureRepository
    {
        private readonly IUserService _userService = userService;
        private readonly IProfilePictureService _profilePictureService = profilePictureService;

        public async Task ChangeProfilePictureAsync(ProfilePictureDTO profilePictureDTO)
        {
            string fileName = await _profilePictureService.SaveFileAsync(profilePictureDTO.ImageFile!);

            int currentUserID = await _userService.GetCurrentUserID();
            User currentUser = await _userService.GetCurrentUserEntityByIdAsync(currentUserID);

            if (!string.IsNullOrEmpty(currentUser.ProfilePicture))
            {
                await _profilePictureService.DeleteProfilePictureAsync(currentUser.ProfilePicture);
            }

            currentUser.ProfilePicture = fileName;

            await _userService.UpdateUserAsync(currentUser, null);
        }

        public async Task<string> GetProfilePictureUrlAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            string profilePictureUrl = await _profilePictureService.GetProfilePictureUrlByUserIdAsync(currentUserID);

            return profilePictureUrl;
        }
    }
}
