using System.IdentityModel.Tokens.Jwt;

namespace prepAIred.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateAcessToken(int userID)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken(int userID)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdFromToken(JwtSecurityToken token)
        {
            throw new NotImplementedException();
        }

        public JwtSecurityToken Verify(string jwtToken)
        {
            throw new NotImplementedException();
        }
    }
}
