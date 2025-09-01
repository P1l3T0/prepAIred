using Microsoft.AspNetCore.Mvc;

namespace prepAIred.API
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
