using prepAIred.Data;

namespace prepAIred.Services
{
    public class UserRepository(IUserService userService) : IUserRepository
    {
        private readonly IUserService _userService = userService;

        public async Task<CurrentUserDTO> GetCurrentUserAsync()
        {
            User user = await _userService.GetCurrentUserAsync();
            return user.ToDto<CurrentUserDTO>();
        }

        public Task DeleteUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
