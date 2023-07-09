using AutoMapper;
using Business.Abstract;
using Business.Utils;
using Core.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;


        public UserService(IUserDal userDal,IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public IDataResult<UserDto> GetUser(int id)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }
        public IDataResult<UserDto> GetUser(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);

            return new SuccessDataResult<UserDto>(_mapper.Map<User, UserDto>(user));
        }

        public IDataResult<IEnumerable<UserDto>> GetAllUsers(int page, int size)
        {
            var users = _userDal.GetAll(null,page,size);

            if (!users.Any())
                return new ErrorDataResult<IEnumerable<UserDto>>(Messages.UserNotFound);

            return new SuccessDataResult<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>> (users));       
        }

        public IResult Delete(int id)
        {
            var userToDelete = _userDal.Get(u => id == u.Id);

            if (userToDelete == null)
                return new ErrorResult(Messages.IdNotFound);

            userToDelete.IsActive = false;
            _userDal.Delete(userToDelete);
            return new SuccessResult(Messages.RemoveSuccess);
        }
    }
}
