using AutoMapper;
using Business.Abstract;
using Business.Helpers.JWT;
using Business.Utils;
using Business.Validation;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using FluentValidation.Results;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly UserForRegisterValidator _validator;


        public UserService(IUserDal userDal, ITokenHelper tokenHelper, IMapper mapper, UserForRegisterValidator validator)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _validator = validator;
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


        public IDataResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _userDal.GetAll();

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

        public IDataResult<Token> Login(UserForLoginDto userForLoginDto,out User userInDb)
        {
            var result = BusinessRules.Run(
             CheckIfEmailNull(userForLoginDto.Email),
             CheckIfPasswordNull(userForLoginDto.Password)
             );

            if (!result.Success)
            {
                userInDb = null;
                return new ErrorDataResult<Token>(result.Message);
            }

            var _userInDb = _userDal.Get(u => u.Email == userForLoginDto.Email);
            if (_userInDb == null)
            {
                userInDb = null;
                return new ErrorDataResult<Token>(Messages.UserNotFound);
            }

            var token = _tokenHelper.CreateToken(_userInDb);

            userInDb = _userInDb;
            return new SuccessDataResult<Token>(token);
        }

        public IDataResult<Token> Register(UserForRegisterDto userForRegisterDto, User newUser)
        {
            var result = BusinessRules.Run(
                CheckIfEmailNull(userForRegisterDto.Email),
                CheckIfPasswordNull(userForRegisterDto.Password),
                CheckIfPhoneNull(userForRegisterDto.Phone)
                );

            if (!result.Success)
                return new ErrorDataResult<Token>(result.Message);

            //UserForRegisterValidator validator = new UserForRegisterValidator();
            //ValidationResult validationResult = validator.Validate(userForRegisterDto);

            ValidationResult validationResult = _validator.Validate(userForRegisterDto);

            if (!validationResult.IsValid)
                return new ErrorDataResult<Token>(validationResult.Errors.FirstOrDefault().ErrorMessage);

                      
            _userDal.Create(newUser);
            var token = _tokenHelper.CreateToken(newUser);

            return new SuccessDataResult<Token>(token, Messages.RegisterSuccess);
        }


        #region Helpers

        private IResult CheckIfEmailNull(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ErrorResult(Messages.EmailCannotBeNull);

            return new SuccessResult();
        }

        private IResult CheckIfPasswordNull(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return new ErrorResult(Messages.PasswordCannotBeNull);

            return new SuccessResult();
        }


        #region UserForRegister
        private IResult CheckIfPhoneNull(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return new ErrorResult(Messages.PhoneCannotBeNull);

            return new SuccessResult();
        }


        #endregion
        #endregion
    }
}
