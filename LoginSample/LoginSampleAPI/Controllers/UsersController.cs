using Business.Abstract;
using Entity.DTOs;
using LoginSampleAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("delete")]
        [Authorize]
        public IActionResult Delete(UserDto userDto)
        {
            var result = _userService.Delete(userDto.Id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public IActionResult GetAll()
        {
            var result = _userService.GetAllUsers();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize]
        public IActionResult Get()
        {
            var id = User.FindFirstValue("name");
            var result = _userService.GetUser(int.Parse(id));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
