using Microsoft.AspNetCore.Mvc;

namespace HahnAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("Register")]
        public IActionResult Register ()
        {
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
