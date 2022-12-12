using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class ConfirmUserMailDtoValidator : AbstractValidator<ConfirmUserMailDto>
    {
        public ConfirmUserMailDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
            RuleFor(x => x.SecretKey).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
        }
    }
}
