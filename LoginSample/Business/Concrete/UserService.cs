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

        public async Task<IDataResult<UserDto>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(u => u.Id == id);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }
        public async Task<IDataResult<UserDto>> GetByEmailAsync(string email)
        {
            var user = await _userDal.GetAsync(u => u.Email == email);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }

        public async Task<IDataResult<IEnumerable<UserDto>>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userDal.GetAllAsync(null,page,size);

            return new SuccessDataResult<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>> (users));       
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var result = BusinessRules.Run(
                await CheckIfUserExistInDbAsync(id),
                CheckIfUserIsAdminOrUserOwner(id)
            );

            if (!result.Success)
                return new ErrorResult(result.Message);

            var userToDelete = await _userDal.GetAsync(u => id == u.Id);

            await _userDal.DeleteAsync(userToDelete);
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

            if (!_userRoleDal.GetUserRolesAsync(loginedUserId).Result.Contains(AuthorizationRoles.Admin)
                && userId != loginedUserId)
                return new ErrorResult(Messages.NotAllowedToDelete);

            return new SuccessResult();
        }

        private async Task<IResult >CheckIfUserExistInDbAsync(int userId)
        {
            var user = await _userDal.GetAsync(u => u.Id == userId);

            if (user == null)
                return new ErrorResult(Messages.UserNotFound);

            return new SuccessResult();
        }
    }
}
