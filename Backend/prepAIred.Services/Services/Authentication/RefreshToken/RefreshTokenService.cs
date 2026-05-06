using prepAIred.Data;
using prepAIred.Exceptions;
using Microsoft.AspNetCore.Http;

namespace prepAIred.Services
{
    public class RefreshTokenService(IHttpContextAccessor httpContextAccessor, IRefreshTokenRepository refreshTokenRepository,
        IJwtService jwtService, ICookieService cookieService, IUserRepository userRepository) : IRefreshTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IJwtService _jwtService = jwtService;
        private readonly ICookieService _cookieService = cookieService;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<RefreshTokenResponseDTO> GenerateNewRefreshTokenAsync()
        {
            string refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["RefreshToken"]!;
            RefreshToken storedToken = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);

            if (storedToken is null || storedToken.ExpiryDate < DateTime.Now || storedToken.IsRevoked)
            {
                throw new InvalidRefreshTokenException("Invalid or expired refresh token.");
            }

            storedToken.IsRevoked = true;

            string newRefreshToken = _jwtService.GenerateRefreshToken(storedToken.UserID);
            string newAccessToken = _jwtService.GenerateAcessToken(storedToken.UserID);

            RefreshToken newRefreshTokenEntity = await _refreshTokenRepository.AddRefreshTokenAsync(new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                UserID = storedToken.UserID,
            });

            CurrentUserDTO currentUser = await _userRepository.GetUserByIdAsync(storedToken.UserID);

            _cookieService.DeleteCookie("RefreshToken");
            _cookieService.CreateCookie("AccessToken", newAccessToken);
            _cookieService.CreateCookie("RefreshToken", newRefreshToken);

            return new RefreshTokenResponseDTO()
            {
                NewAccessToken = newAccessToken,
                NewRefreshToken = newRefreshToken,
                ExpiresIn = 600,
                Username = currentUser.Username
            };
        }
    }
}
