using Business.Abstract;
using Business.Utils;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public IActionResult Create(Role role)
        {
            var result = _roleService.Create(role);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int roleId)
        {
            var result = _roleService.Delete(roleId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update")]
        public IActionResult Update(int roleId, Role newRole)
        {
            var result = _roleService.Update(roleId, newRole);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
