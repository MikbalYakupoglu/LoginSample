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
            User newUser = HashPasswordAndCreateUser(userForRegisterDto);
            var result = _userService.Register(userForRegisterDto, newUser);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            User userInDb;
            var result = _userService.Login(userForLoginDto, out userInDb);

            if (!result.Success)
                return BadRequest(result);

            var passwordResult = PasswordHasher.VerifyPassword(userForLoginDto.Password, userInDb.PasswordHash, userInDb.PasswordSalt);
            if (!passwordResult)
                return BadRequest(Messages.IncorrectPassword);

            return Ok(result);
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

        [HttpGet("verify")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok();
        }




        private static User HashPasswordAndCreateUser(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHasher.HashPassword(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var newUser = new User
            {
                IsActive = true,
                Email = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return newUser;
        }
    }
}
