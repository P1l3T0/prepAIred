using prepAIred.Data;

namespace prepAIred.Services
{
    public class UserService(IUserRepository userRepository, ICookieService cookieService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICookieService _cookieService = cookieService;

        public async Task<CurrentUserDTO> GetCurrentUserAsync()
        {
            CurrentUserDTO user = await _userRepository.GetCurrentUserAsync();
            return user;
        }

        public async Task UpdateCurrentUserAsync(UserCredentialsDTO userCredentialsDto)
        {
            await _userRepository.ValidateUpdateUserDataAsync(userCredentialsDto);

            int currentUserID = await _userRepository.GetCurrentUserID();
            User currentUser = await _userRepository.GetCurrentUserEntityByIdAsync(currentUserID);

            await _userRepository.UpdateUserAsync(currentUser, userCredentialsDto);
        }

        public async Task DeleteCurrentUserAsync()
        {
            _cookieService.DeleteCookie("AccessToken");
            _cookieService.DeleteCookie("RefreshToken");

            int currentUserID = await _userRepository.GetCurrentUserID();
            await _userRepository.DeleteUserAsync(currentUserID);
        }
    }
}
