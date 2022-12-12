using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class UserRegisterHistoryDtoValidator : AbstractValidator<UserRegisterHistoryDto>
    {
        public UserRegisterHistoryDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("{PropertyName} must be greather than 0!");
        }
    }
}
