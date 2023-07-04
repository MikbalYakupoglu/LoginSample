using Business.Abstract;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var result = _userService.Register(userForRegisterDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _userService.Login(userForLoginDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("remove")]
        [Authorize]
        public IActionResult Remove(int id)
        {
            var result = _userService.Remove(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        [Authorize]
        public IActionResult GetAll(int id)
        {
            var result = _userService.GetAllUsers();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize]
        public IActionResult Get(string email)
        {
            var result = _userService.GetUser(email);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


    }
}
