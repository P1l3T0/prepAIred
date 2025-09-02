using prepAIred.Data;

namespace prepAIred.Services
{
    public class AuthService(IJwtService jwtService, IRefreshTokenService refreshTokenService, ICookieService cookieService, IUserService userService) : IAuthService
    {
        private readonly IJwtService _jwtService = jwtService;
        private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
        private readonly ICookieService _cookieService = cookieService;
        private readonly IUserService _userService = userService;

        public async Task<User> RegisterAsync(RegisterDTO registerDto, byte[] hashedPassword, byte[] saltPassword)
        {
            return await _userService.CreateUserAsync(new User()
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                PasswordHash = hashedPassword,
                PasswordSalt = saltPassword
            });
        }

        public async Task<User> LoginAsync(LoginDTO loginDto)
        {
            if (!await _userService.UserExistsAsync(loginDto.Email)) throw new Exception("User does not exist");

            User user = await _userService.GetUserByEmailAsync(loginDto.Email);

            if (!_userService.CheckPassword(user, loginDto)) throw new Exception("Invalid Password");

            return user;
        }

        public async Task GenerateAuthResponse(User user)
        {
            string accessToken = _jwtService.GenerateAcessToken(user.ID);
            string refreshToken = _jwtService.GenerateRefreshToken(user.ID);

            await _refreshTokenService.AddRefreshTokenAsync(new RefreshToken()
            {
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(1),
                UserID = user.ID
            });

            _cookieService.CreateCookie("AccessToken", accessToken);
            _cookieService.CreateCookie("RefreshToken", refreshToken);
        }

        public Task Logout()
        {
            _cookieService.DeleteCookie("AccessToken");
            _cookieService.DeleteCookie("RefreshToken");

            return Task.CompletedTask;
        }
    }
}
