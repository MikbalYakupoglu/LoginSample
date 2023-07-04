using Business.Abstract;
using Business.Utils;
using Business.Utils.JWT;
using Business.Validation;
using Core.Results;
using Core.Utils;
using DataAccess.Abstract;
using Entity;
using Entity.DTOs;
using FluentValidation.Results;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;


        public UserService(IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> GetUser(int id)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }
        public IDataResult<User> GetUser(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }


        public IDataResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userDal.GetAll();

            if (!users.Any())
                return new ErrorDataResult<IEnumerable<User>>(Messages.UserNotFound);

            return new SuccessDataResult<IEnumerable<User>>(users);       
        }

        public IResult Remove(int id)
        {
            var userToDelete = _userDal.Get(u => id == u.Id);

            if (userToDelete == null)
                return new ErrorResult(Messages.IdNotFound);

            userToDelete.IsActive = false;
            _userDal.Remove(userToDelete);
            return new SuccessResult();
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

        public IResult Register(UserForRegisterDto userForRegisterDto)
        {
            var result = BusinessRules.Run(
                CheckIfEmailNull(userForRegisterDto.Email),
                CheckIfPasswordNull(userForRegisterDto.Password),
                CheckIfPhoneNull(userForRegisterDto.Phone)
                );

            if (!result.Success)
                return new ErrorResult(result.Message);

            UserForRegisterValidator validator = new UserForRegisterValidator();
            ValidationResult validationResult = validator.Validate(userForRegisterDto);

            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FirstOrDefault().ErrorMessage);


            byte[] passwordHash, passwordSalt;
            PasswordHasher.HashPassword(userForRegisterDto.Password,out passwordHash, out passwordSalt);

            _userDal.Create(new User
            {
                IsActive = true,
                Email = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            return new SuccessResult("Kayıt Başarılı");
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
