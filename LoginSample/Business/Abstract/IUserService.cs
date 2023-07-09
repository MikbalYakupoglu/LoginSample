using Core.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<IEnumerable<UserDto>> GetAllUsers(int page, int size);
        IDataResult<UserDto> GetUser(int id);
        IDataResult<UserDto> GetUser(string email);
        IResult Delete(int id);
    }
}
