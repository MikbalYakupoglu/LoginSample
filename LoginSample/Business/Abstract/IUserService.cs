using Core.Results;
using Entity;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<IEnumerable<UserDto>> GetAllUsers();
        IDataResult<UserDto> GetUser(int id);
        IDataResult<UserDto> GetUser(string email);
        IDataResult<Token> Register(UserForRegisterDto userToRegisterDto, User newUser);
        IDataResult<Token> Login(UserForLoginDto userToLoginDto, out User userInDb);
        IResult Remove(int id);
    }
}
