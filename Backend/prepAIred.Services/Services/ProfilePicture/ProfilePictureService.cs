using prepAIred.Data;

namespace prepAIred.Services
{
    public class ProfilePictureService(IProfilePictureRepository profilePictureRepository, IUserRepository userRepository) : IProfilePictureService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProfilePictureRepository _profilePictureRepository = profilePictureRepository;

        public async Task ChangeProfilePictureAsync(ProfilePictureDTO profilePictureDTO)
        {
            string fileName = await _profilePictureRepository.SaveFileAsync(profilePictureDTO.ImageFile!);

            int currentUserID = await _userRepository.GetCurrentUserID();
            User currentUser = await _userRepository.GetCurrentUserEntityByIdAsync(currentUserID);

            if (!string.IsNullOrEmpty(currentUser.ProfilePicture))
            {
                await _profilePictureRepository.DeleteProfilePictureAsync(currentUser.ProfilePicture);
            }

            currentUser.ProfilePicture = fileName;

            await _userRepository.UpdateUserAsync(currentUser, null);
        }

        public async Task<string> GetProfilePictureUrlAsync()
        {
            int currentUserID = await _userRepository.GetCurrentUserID();
            string profilePictureUrl = await _profilePictureRepository.GetProfilePictureUrlByUserIdAsync(currentUserID);

            return profilePictureUrl;
        }
    }
}
