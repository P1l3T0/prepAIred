using prepAIred.Data;

namespace prepAIred.Services
{
    public class AuthRepository(IAuthService authService, IUserService userService) : IAuthRepository
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;

        public async Task RegisterAsync(UserCredentialsDTO userCredentialsDto)
        {
            await _userService.ValidateUserAsync(userCredentialsDto);

            (byte[] hashedPassword, byte[] saltPassword) = _userService.HashPassword(userCredentialsDto);

            CurrentUserDTO currentUser = await _authService.RegisterAsync(userCredentialsDto, hashedPassword, saltPassword);

            await _authService.GenerateAuthResponseAsync(currentUser);
        }

        public async Task LoginAsync(LoginDTO loginDto)
        {
            CurrentUserDTO currentUser = await _authService.LoginAsync(loginDto);

            await _authService.GenerateAuthResponseAsync(currentUser);
        }

        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();

            await Task.CompletedTask;
        }
    }
}
