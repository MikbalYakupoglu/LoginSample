using AutoMapper;
using Business.Abstract;
using Business.Helpers.JWT;
using Business.Utils;
using Business.Validation;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity;
using Entity.Abstract;
using Entity.DTOs;
using FluentValidation.Results;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;


        public UserService(IUserDal userDal, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
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


        public IDataResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _userDal.GetAll();

            if (!users.Any())
                return new ErrorDataResult<IEnumerable<UserDto>>(Messages.UserNotFound);

            return new SuccessDataResult<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>> (users));       
        }

        public IResult Remove(int id)
        {
            var userToDelete = _userDal.Get(u => id == u.Id);

            if (userToDelete == null)
                return new ErrorResult(Messages.IdNotFound);

            userToDelete.IsActive = false;
            _userDal.Remove(userToDelete);
            return new SuccessResult("Kullanıcı Başarıyla Silindi");
        }

        public IDataResult<Token> Login(UserForLoginDto userForLoginDto)
        {
            var result = BusinessRules.Run(
             CheckIfEmailNull(userForLoginDto.Email),
             CheckIfPasswordNull(userForLoginDto.Password)
             );

            if (!result.Success)
                return new ErrorDataResult<Token>(result.Message);

            var userInDB = _userDal.Get(u => u.Email == userForLoginDto.Email);
            if (userInDB == null)
                return new ErrorDataResult<Token>(Messages.UserNotFound);

            var passwordResult = PasswordHasher.VerifyPassword(userForLoginDto.Password, userInDB.PasswordHash, userInDB.PasswordSalt);
            if (!passwordResult)
                return new ErrorDataResult<Token>(Messages.IncorrectPassword);

            var token = _tokenHelper.CreateToken(userInDB);

            return new SuccessDataResult<Token>(token);
        }

        public IDataResult<Token> Register(UserForRegisterDto userForRegisterDto)
        {
            var result = BusinessRules.Run(
                CheckIfEmailNull(userForRegisterDto.Email),
                CheckIfPasswordNull(userForRegisterDto.Password),
                CheckIfPhoneNull(userForRegisterDto.Phone)
                );

            if (!result.Success)
                return new ErrorDataResult<Token>(result.Message);

            UserForRegisterValidator validator = new UserForRegisterValidator();
            ValidationResult validationResult = validator.Validate(userForRegisterDto);

            if (!validationResult.IsValid)
                return new ErrorDataResult<Token>(validationResult.Errors.FirstOrDefault().ErrorMessage);


            byte[] passwordHash, passwordSalt;
            PasswordHasher.HashPassword(userForRegisterDto.Password,out passwordHash, out passwordSalt);

            var newUser = new User
            {
                IsActive = true,
                Email = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userDal.Create(newUser);

            var token = _tokenHelper.CreateToken(newUser);

            return new SuccessDataResult<Token>(token,"Kayıt Başarılı");
        }


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
    }
}
