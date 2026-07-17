using DeveloperLandingApi.Application.DTOs;
using FluentValidation;

namespace DeveloperLandingApi.Application.Validators
{
    public class ContactRequestValidator : AbstractValidator<ContactRequestDto>
    {
        public ContactRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required")
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email");

            RuleFor(x => x.Comment)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
