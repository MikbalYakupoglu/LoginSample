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
        IDataResult<Token> Register(UserForRegisterDto userToRegisterDto);
        IDataResult<Token> Login(UserForLoginDto userToLoginDto);
        IResult Remove(int id);
    }
}
