using FluentValidation;

namespace OrderingApplication.Feutures.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("The {Username} should not be empty.")
                .NotNull()
                .MaximumLength(40).WithMessage("{Username} Maximum lenght should be less than or equal to 40.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} should not be empty.");
        }
    }
}
