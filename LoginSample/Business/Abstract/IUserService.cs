using Core.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<IEnumerable<UserDto>>> GetAllUsersAsync(int page, int size);
        Task<IDataResult<UserDto>> GetByIdAsync(int id);
        Task<IDataResult<UserDto>> GetByEmailAsync(string email);
        Task<IResult> DeleteAsync(int id);
    }
}
