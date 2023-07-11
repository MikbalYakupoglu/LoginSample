using Azure;
using Core.Results;
using Entity.Concrete;

namespace Business.Abstract;

public interface IRoleService
{
    Task<IResult> CreateAsync(Role role);
    Task<IResult> DeleteAsync(int roleId);
    Task<IResult> UpdateAsync(int roleId, Role newRole);
    Task<IDataResult<Role>> GetAsync(int roleId);
    Task<IDataResult<IEnumerable<Role>>> GetAllAsync();

}