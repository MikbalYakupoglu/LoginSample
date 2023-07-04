using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class MainController : ControllerBase
    {
        [HttpGet("Test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authorized User Accessed.");
        }
    }
}
