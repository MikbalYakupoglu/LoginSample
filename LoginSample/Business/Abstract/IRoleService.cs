using Azure;
using Core.Results;
using Entity.Concrete;

namespace Business.Abstract;

public interface IRoleService
{
    IResult Create(Role role);
    IResult Delete(int roleId);
    IResult Update(int roleId, Role newRole);
    IDataResult<Role> Get(int roleId);
    IDataResult<IEnumerable<Role>> GetAll();

}