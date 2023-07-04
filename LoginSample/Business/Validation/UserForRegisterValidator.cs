using Entity.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class UserForRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            RuleFor(user => user.Email).Must(u => u.Contains('@')).WithMessage("Email @ işareti içermelidir.");
            RuleFor(user => user.Email).MaximumLength(100);

            RuleFor(user => user.Password).MinimumLength(8).WithMessage("Şifre En Az 8 karakter olmadılır.");

            RuleFor(user => user.Phone).Length(10).WithMessage("Telefon 10 haneli olmalıdır.");
        }
    }
}
