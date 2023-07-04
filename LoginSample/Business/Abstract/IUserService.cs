using Core.Results;
using Entity;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<IEnumerable<User>> GetAllUsers();
        IDataResult<User> GetUser(int id);
        IDataResult<User> GetUser(string email);
        IResult Register(UserForRegisterDto userToRegisterDto);
        IDataResult<Token> Login(UserForLoginDto userToLoginDto);
        IResult Remove(int id);
    }
}
