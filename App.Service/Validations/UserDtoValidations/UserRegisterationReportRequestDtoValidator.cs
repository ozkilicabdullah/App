using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations.UserDtoValidations
{

    public class UserRegisterationReportRequestDtoValidator:AbstractValidator<UserRegisterationReportRequestDto>
    {
        public UserRegisterationReportRequestDtoValidator()
        {
            RuleFor(x => x.BeginDate).NotNull().WithMessage("{Property Name} is required!")
               .NotEmpty().WithMessage("{Property Name} is required!");
            RuleFor(x => x.EndDate).NotNull().WithMessage("{Property Name} is required!")
                .NotEmpty().WithMessage("{Property Name} is required!");
        }
    }
}
