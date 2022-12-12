using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
