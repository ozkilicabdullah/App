using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
