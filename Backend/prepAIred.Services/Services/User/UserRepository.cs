using prepAIred.Data;

namespace prepAIred.Services
{
    public class UserRepository(IUserService userService, ICookieService cookieService) : IUserRepository
    {
        private readonly IUserService _userService = userService;
        private readonly ICookieService _cookieService = cookieService;

        public async Task<CurrentUserDTO> GetCurrentUserAsync()
        {
            CurrentUserDTO user = await _userService.GetCurrentUserAsync();
            return user;
        }

        public async Task UpdateCurrentUserAsync(UpdateUserDTO updateUserDTO)
        {
            await _userService.ValidateUpdateUserDataAsync(updateUserDTO);

            int currentUserID = await _userService.GetCurrentUserID();
            User currentUser = await _userService.GetCurrentUserEntityByIdAsync(currentUserID);

            await _userService.UpdateUserAsync(currentUser, updateUserDTO);
        }

        public async Task DeleteCurrentUserAsync()
        {
            _cookieService.DeleteCookie("AccessToken");
            _cookieService.DeleteCookie("RefreshToken");

            int currentUserID = await _userService.GetCurrentUserID();
            await _userService.DeleteUserAsync(currentUserID);
        }
    }
}
