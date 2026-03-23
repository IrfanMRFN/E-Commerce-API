using FluentValidation;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .WithMessage("Product ID is required.");
    }
}
