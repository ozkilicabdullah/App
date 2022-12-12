using App.Core.Dto;
using FluentValidation;
namespace App.Service.Validations.UserDtoValidations
{
    public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
        }
    }
}
