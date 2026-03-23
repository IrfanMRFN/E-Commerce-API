using FluentValidation;

namespace ECommerce.Application.Features.Products.Commands.AddStock;

public class AddStockCommandValidator : AbstractValidator<AddStockCommand>
{
    public AddStockCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity to add must be greater than zero.");
    }
}
