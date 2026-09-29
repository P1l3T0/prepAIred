using prepAIred.Data;

namespace prepAIred.Services
{
    public class AuthService(IAuthRepository authRepository, IUserRepository userRepository) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task RegisterAsync(UserCredentialsDTO userCredentialsDto)
        {
            await _userRepository.ValidateUserAsync(userCredentialsDto);

            (byte[] hashedPassword, byte[] saltPassword) = _userRepository.HashPassword(userCredentialsDto);

            CurrentUserDTO currentUser = await _authRepository.RegisterAsync(userCredentialsDto, hashedPassword, saltPassword);

            await _authRepository.GenerateAuthResponseAsync(currentUser);
        }

        public async Task LoginAsync(LoginDTO loginDto)
        {
            CurrentUserDTO currentUser = await _authRepository.LoginAsync(loginDto);

            await _authRepository.GenerateAuthResponseAsync(currentUser);
        }

        public async Task LogoutAsync()
        {
            await _authRepository.LogoutAsync();

            await Task.CompletedTask;
        }
    }
}
