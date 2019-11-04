using Microsoft.AspNetCore.Mvc;

namespace Newsfeed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        public PingController() { }

        [HttpGet]
        public IActionResult Ping()
        {
            return new OkObjectResult("Ping ping, server staat online!");
        }
    }
}