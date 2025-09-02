using prepAIred.Data;
using System.Security.Claims;

namespace prepAIred.Services
{
    public class UserService : IUserService
    {
        public bool AreFieldsEmpty(RegisterDTO registerDto)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(User user, LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserUsername(ClaimsPrincipal userPrincipal)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public (byte[] hashedPassword, byte[] saltPassword) HashPassword(RegisterDTO registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public bool ValidateEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task ValidateUserAsync(RegisterDTO registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
