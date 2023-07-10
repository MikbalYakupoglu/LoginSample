using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Data;

namespace Business.Concrete;

public class RoleService : IRoleService
{
    private readonly IRoleDal _roleDal;

    public RoleService(IRoleDal roleDal)
    {
        _roleDal = roleDal;
    }

    public IResult Create(Role role)
    {
        var result = BusinessRules.Run(
            CheckIfInputsNull(role.Name),
            CheckIfRoleAlreadyExist(role.Name)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        _roleDal.Create(role);
        return new SuccessResult(Messages.RoleCreateSuccess);
    }

    public IResult Delete(int roleId)
    {
        var result = BusinessRules.Run(
            CheckIfRoleExistInDbForModify(roleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var role = _roleDal.Get(r => r.Id == roleId);
        _roleDal.Delete(role);
        return new SuccessResult(Messages.RoleDeleteSuccess);
    }

    public IResult Update(int roleId, Role newRole)
    {
        var result = BusinessRules.Run(
            CheckIfRoleExistInDbForModify(roleId)
        );

        if (!result.Success)
            return new ErrorResult(result.Message);

        var role = _roleDal.Get(r => r.Id == roleId);
        role.Name = newRole.Name;
        _roleDal.Update(role);
        return new SuccessResult(Messages.RoleUpdateSuccess);
    }

    public IDataResult<Role> Get(int roleId)
    {
        var role = _roleDal.Get(r => r.Id == roleId);

        if (role == null)
            return new ErrorDataResult<Role>(Messages.RoleNotFound);

        return new SuccessDataResult<Role>(role);
    }

    public IDataResult<IEnumerable<Role>> GetAll()
    {
        var roles = _roleDal.GetAll(null);

        return new SuccessDataResult<IEnumerable<Role>>(roles);
    }

    private IResult CheckIfRoleAlreadyExist(string roleName)
    {
        var role = _roleDal.Get(r => r.Name == roleName);

        if (role != null)
            return new ErrorResult(Messages.RoleAlreadyExist);

        return new SuccessResult();
    }

    private IResult CheckIfRoleExistInDbForModify(int roleId)
    {
        var role = _roleDal.Get(r => r.Id == roleId);

        if (role == null)
            return new ErrorResult(Messages.RoleNotFound);

        return new SuccessResult();
    }

    private IResult CheckIfInputsNull(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return new ErrorResult(Messages.RoleNameCannotBeNull);

        return new SuccessResult();
    }
}