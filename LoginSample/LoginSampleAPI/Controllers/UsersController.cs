using Business.Abstract;
using Business.Concrete;
using Entity.DTOs;
using Business.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public UsersController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
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

        [HttpGet("getlogineduser")]
        [Authorize]
        public IActionResult GetLoginedUser()
        {
            var id = User.FindFirstValue(JwtRegisteredClaimNames.Name);
            var result = _userService.GetUser(int.Parse(id ?? throw new ArgumentNullException()));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public IActionResult GetUser(int id)
        {
            var result = _userService.GetUser(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public IActionResult GetAll(int page = 0, int size = 25)
        {
            var result = _userService.GetAllUsers(page,size);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addrole")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public IActionResult AddRole([FromForm]int userId,[FromForm] int[] roleIds)
        {
            var result = _userRoleService.AddRoleToUser(userId, roleIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("removerole")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public IActionResult RemoveRole([FromForm]int userId,[FromForm] int[] roleIds)
        {
            var result = _userRoleService.RemoveRoleFromUser(userId, roleIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
