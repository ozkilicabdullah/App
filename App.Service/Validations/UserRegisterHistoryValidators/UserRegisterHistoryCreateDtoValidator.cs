using App.Core.Dto;
using FluentValidation;

namespace App.Service.Validations
{
    public class UserRegisterHistoryCreateDtoValidator : AbstractValidator<UserRegisterHistoryCreateDto>
    {
        public UserRegisterHistoryCreateDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("{PropertyName} must be greather than 0 !");
        }
    }
}
