using EGS.Application.Common.Interfaces;
using FluentValidation;
namespace EGS.Application.Users.Commands.Create
{
    public class RegisterCustomerCommandValidator:AbstractValidator<RegisterCustomerCommand>
    {
        private readonly IIdentityService _identityService;
        public RegisterCustomerCommandValidator(IIdentityService identityService)
        {
            _identityService = identityService;
            
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is requierd")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Equal(u => u.ConfirmedPassword).WithMessage("Password and cofirmation don't match");

        }
    }
}
