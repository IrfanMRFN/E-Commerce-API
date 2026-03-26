using FluentValidation;

namespace ECommerce.Application.Features.Customers.Queries.LoginCustomer;

public class LoginCustomerQueryValidator : AbstractValidator<LoginCustomerQuery>
{
    public LoginCustomerQueryValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().EmailAddress().MaximumLength(100);

        RuleFor(c => c.Password)
            .NotEmpty().MinimumLength(6).MaximumLength(50);
    }
}
