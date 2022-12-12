using App.Core.Dto;
using FluentValidation;
namespace App.Service.Validations
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} is required!");
        }
    }
}
