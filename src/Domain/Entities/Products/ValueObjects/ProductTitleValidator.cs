using FluentValidation;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

internal class ProductTitleValidator : AbstractValidator<ProductTitle>
{
    private const int MinLength = 5;
    private const int MaxLength = 350;

    public ProductTitleValidator()
    {
        RuleFor(productTitle => productTitle.Value)
            .NotEmpty()
                .WithErrorCode("Product_Title_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Product_Title_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Product_Title_MaximumLength");
    }
}
