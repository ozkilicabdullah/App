using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
