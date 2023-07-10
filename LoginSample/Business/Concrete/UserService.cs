using System.Security.Claims;
using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Entity.Temp;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IUserRoleDal _userRoleDal;


        public UserService(IUserDal userDal,IMapper mapper, IUserRoleDal userRoleDal)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userRoleDal = userRoleDal;
        }

        public IDataResult<UserDto> GetById(int id)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }
        public IDataResult<UserDto> GetByEmail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }

        public IDataResult<IEnumerable<UserDto>> GetAllUsers(int page, int size)
        {
            var users = _userDal.GetAll(null,page,size);

            return new SuccessDataResult<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>> (users));       
        }

        public IResult Delete(int id)
        {
            var result = BusinessRules.Run(
                CheckIfUserExistInDb(id),
                CheckIfUserIsAdminOrUserOwner(id)
            );

            if (!result.Success)
                return new ErrorResult(result.Message);

            var userToDelete = _userDal.Get(u => id == u.Id);

            _userDal.Delete(userToDelete);
            return new SuccessResult(Messages.RemoveSuccess);
        }

        private int GetLoginedUserId()
        {
            var loginedUserId = LoginedUser.ClaimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Name);
            return int.Parse(loginedUserId);
        }

        private IResult CheckIfUserIsAdminOrUserOwner(int userId)
        {
            var loginedUserId = GetLoginedUserId();

            if (!_userRoleDal.GetUserRoles(loginedUserId).Contains(AuthorizationRoles.Admin)
                && userId != loginedUserId)
                return new ErrorResult(Messages.NotAllowedToDelete);

            return new SuccessResult();
        }

        private IResult CheckIfUserExistInDb(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);

            if (user == null)
                return new ErrorResult(Messages.UserNotFound);

            return new SuccessResult();
        }
    }
}
