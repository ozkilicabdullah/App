using App.Core.Dto;
using FluentValidation;
namespace App.Service.Validations.UserDtoValidations
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MinimumLength(6).WithMessage("{PropertyName} must be minimum length 6"); ;
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MinimumLength(6).WithMessage("{PropertyName} must be minimum length 6");
        }
    }
}
