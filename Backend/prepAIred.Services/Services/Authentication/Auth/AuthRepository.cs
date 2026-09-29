using prepAIred.Data;
using prepAIred.Exceptions;

namespace prepAIred.Services
{
    public class AuthRepository(IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository, ICookieService cookieService, IUserRepository userRepository) : IAuthRepository
    {
        private readonly IJwtService _jwtService = jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly ICookieService _cookieService = cookieService;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<CurrentUserDTO> RegisterAsync(UserCredentialsDTO userCredentialsDto, byte[] hashedPassword, byte[] saltPassword)
        {
            User newUser = new User()
            {
                Email = userCredentialsDto.Email,
                Username = userCredentialsDto.Username,
                PasswordHash = hashedPassword,
                PasswordSalt = saltPassword
            };

            await _userRepository.CreateUserAsync(newUser);

            return newUser.ToDto<CurrentUserDTO>();
        }

        public async Task<CurrentUserDTO> LoginAsync(LoginDTO loginDto)
        {
            if (!await _userRepository.UserExistsAsync(loginDto.Email)) throw new ResourceNotFoundException("Invalid Username or Password");

            User currentUser = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (!_userRepository.CheckPassword(currentUser, loginDto)) throw new InvalidCredentialsException("Invalid Username or Password");

            return currentUser.ToDto<CurrentUserDTO>();
        }

        public async Task GenerateAuthResponseAsync(CurrentUserDTO currentUser)
        {
            string accessToken = _jwtService.GenerateAcessToken(currentUser.ID);
            string refreshToken = _jwtService.GenerateRefreshToken(currentUser.ID);

            await _refreshTokenRepository.AddRefreshTokenAsync(new RefreshToken()
            {
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(1),
                UserID = currentUser.ID
            });

            _cookieService.CreateCookie("AccessToken", accessToken);
            _cookieService.CreateCookie("RefreshToken", refreshToken);
        }

        public async Task LogoutAsync()
        {
            _cookieService.DeleteCookie("AccessToken");
            _cookieService.DeleteCookie("RefreshToken");

            await Task.CompletedTask;
        }
    }
}
