using prepAIred.Data;

namespace prepAIred.Services
{
    public class AuthService : IAuthService
    {
        public async Task<User> RegisterAsync(RegisterDTO registerDto, byte[] hashedPassword, byte[] saltPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task GenerateAuthResponse(User user)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
