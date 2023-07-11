using Business.Abstract;
using Business.Concrete;
using Entity.DTOs;
using Business.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Core.Entity.Temp;

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


        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> Delete(UserDto userDto)
        {
            var result = await _userService.DeleteAsync(userDto.Id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getlogineduser")]
        [Authorize]
        public async Task<IActionResult> GetLoginedUser()
        {
            var id = User.FindFirstValue(JwtRegisteredClaimNames.Name);
            var result = await _userService.GetByIdAsync(int.Parse(id ?? throw new ArgumentNullException()));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> GetAll(int page = 0, int size = 25)
        {
            var result = await _userService.GetAllUsersAsync(page, size);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addrole")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> AddRole([FromForm] int userId, [FromForm] int[] roleIds)
        {
            var result = await _userRoleService.AddRoleToUserAsync(userId, roleIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("removerole")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> RemoveRole([FromForm] int userId, [FromForm] int[] roleIds)
        {
            var result = await _userRoleService.RemoveRoleFromUserAsync(userId, roleIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
