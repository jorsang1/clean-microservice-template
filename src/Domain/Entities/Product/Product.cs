using CleanCompanyName.DDDMicroservice.Domain.Common;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Product.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Product;

public class Product : AuditableEntity, IValidatable
{
    public Guid Id { get; init; }
    public string Sku { get; init; }
    public ProjectTitle Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }

    public ValidationResult Validate()
    {
        var validationResult = new ValidationResult();

        if (Id == Guid.Empty)
        {
            validationResult.AddError("", "ID cannot be empty", "");
        }

        validationResult.AddErrorsFrom(Title);

        if (String.IsNullOrEmpty(Sku))
        {
            validationResult.AddError("", "SKU cannot be empty", "");
        }
        if (String.IsNullOrEmpty(Description))
        {
            validationResult.AddError("", "Description cannot be empty", "");
        }
        if (Price < 0)
        {
            validationResult.AddError("", "Price can't be negative", "");
        }

        return validationResult;
    }
}