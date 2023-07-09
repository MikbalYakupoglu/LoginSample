using Core.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<IEnumerable<UserDto>> GetAllUsers(int page, int size);
        IDataResult<UserDto> GetById(int id);
        IDataResult<UserDto> GetByEmail(string email);
        IResult Delete(int id);
    }
}
