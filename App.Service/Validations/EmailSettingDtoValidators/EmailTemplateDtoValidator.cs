using App.Core.Models;
using FluentValidation;

namespace App.Service.Validations.EmailSettingDtoValidators
{
    public class EmailTemplateDtoValidator : AbstractValidator<EmailTemplate>
    {
        public EmailTemplateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
