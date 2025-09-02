using Microsoft.AspNetCore.Http;

namespace prepAIred.Services
{
    public class CookieService(IHttpContextAccessor httpContextAccessor) : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public void CreateCookie(string name, string value)
        {
            _httpContextAccessor.HttpContext!.Response.Cookies.Append(name, value, new CookieOptions()
            {
                HttpOnly = name == "AccessToken" ? true : false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Domain = "localhost",
                Path = "/",
                Expires = name == "AccessToken"
                    ? DateTime.Now.AddSeconds(1200)
                    : DateTime.Now.AddDays(5),
            });
        }

        public void DeleteCookie(string name)
        {
            _httpContextAccessor.HttpContext!.Response.Cookies.Delete(name, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Domain = "localhost",
                Path = "/"
            });
        }
    }
}
