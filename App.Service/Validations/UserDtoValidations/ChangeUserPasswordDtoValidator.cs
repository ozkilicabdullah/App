using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class ChangeUserPasswordDtoValidator : AbstractValidator<ChangeUserPasswordDto>
    {
        public ChangeUserPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
            RuleFor(x => x.Password).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
        }
    }
}
