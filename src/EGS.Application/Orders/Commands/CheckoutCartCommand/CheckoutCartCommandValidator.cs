using FluentValidation;

namespace EGS.Application.Orders.Commands.CheckoutCartCommand
{
    public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
    {
        public CheckoutCartCommandValidator()
        {
            RuleFor(c => c.Comment)
                .MaximumLength(250).WithMessage("Entered comment is too long");
        }
    }
}
