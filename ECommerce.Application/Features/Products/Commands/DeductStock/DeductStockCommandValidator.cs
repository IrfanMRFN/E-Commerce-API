using FluentValidation;

namespace ECommerce.Application.Features.Products.Commands.DeductStock;

public class DeductStockCommandValidator : AbstractValidator<DeductStockCommand>
{
    public DeductStockCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity to deduct must be greater than zero.");
    }
}
