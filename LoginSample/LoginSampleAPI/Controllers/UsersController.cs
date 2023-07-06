using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using Entity;
using Entity.DTOs;
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


        [HttpPost("remove")]
        [Authorize]
        public IActionResult Remove(UserDto userDto)
        {
            var result = _userService.Remove(userDto.Id);

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
        public IActionResult Get()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userService.GetUser(email);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
