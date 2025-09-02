using prepAIred.Data;

namespace prepAIred.Services
{
    public class UserRepository : IUserRepository
    {
        public Task<CurrentUserDTO> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(int userID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
