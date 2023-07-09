using Business.Abstract;
using Business.Utils;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;
using System.Diagnostics.Metrics;

namespace Business.Concrete
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        private readonly IRoleDal _roleDal;

        public UserRoleService(IUserRoleDal userRoleDal, IRoleDal roleDal)
        {
            _userRoleDal = userRoleDal;
            _roleDal = roleDal;
        }

        // Kullanıcıda Zaten Eklenmek İstenen Rol Bulunmuyorsa ekle.
        public IResult AddRoleToUser(int userId, List<int> roleIdsToAdd)
        {
            var result = BusinessRules.Run(
                    CheckIfRolesExist(roleIdsToAdd)
                );

            if (!result.Success)
                return new ErrorResult(result.Message);

            int addedRoleCount = AddUserRoles(userId, roleIdsToAdd);

            if (addedRoleCount == 0)
                return new SuccessResult(Messages.UserRoleNotModified);

            return new SuccessResult(Messages.UserRoleUpdateSuccess);
        }

        private int AddUserRoles(int userId, List<int> roleIdsToAdd)
        {
            var userRoles = _userRoleDal.GetAll(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
            int addedRoleCount = 0;

            foreach (var roleToAdd in roleIdsToAdd)
            {
                if (!userRoles.Contains(roleToAdd))
                {
                    _userRoleDal.Create(new UserRole
                    {
                        UserId = userId,
                        RoleId = roleToAdd
                    });
                    addedRoleCount++;
                }
            }

            return addedRoleCount;
        }

        // Kullanıcıda Silinmek İstenen Rol Bulunuyorsa Sil.
        public IResult RemoveRoleFromUser(int userId, List<int> roleIdsToRemove)
        {
            int deletedRoleCount = RemoveUserRoles(userId, roleIdsToRemove);

            if (deletedRoleCount == 0)
                return new SuccessResult(Messages.UserRoleNotModified);

            return new SuccessResult(Messages.UserRoleUpdateSuccess);
        }

        private int RemoveUserRoles(int userId, List<int> roleIdsToRemove)
        {
            var userRoles = _userRoleDal.GetAll(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
            int deletedRoleCount = 0;
            foreach (var roleToDelete in roleIdsToRemove)
            {
                if (userRoles.Contains(roleToDelete))
                {
                    var userRoleToDelete = _userRoleDal.Get(ur => ur.UserId == userId && ur.RoleId == roleToDelete);
                    _userRoleDal.Delete(userRoleToDelete);
                    deletedRoleCount++;
                }
            }

            return deletedRoleCount;
        }

        private IResult CheckIfRolesExist(List<int> roleIdsToAdd)
        {
            var roleIds = _roleDal.GetAll(null).Select(r => r.Id);
            var isRolesCorrect = roleIdsToAdd.All(id => roleIds.Contains(id));

            if (!isRolesCorrect)
                return new ErrorResult(Messages.RoleNotFound);

            return new SuccessResult();
        }
    }
}
