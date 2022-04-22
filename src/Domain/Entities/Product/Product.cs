using Domain.Common;
using Domain.Entities.Product.ValueObjects;

namespace Domain.Entities.Product;

public class Product : AuditableEntity
{
    public Guid Id { get; init; }
    public string Sku { get; init; }
    public ProjectTitle Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }

    protected override void Validate()
    {
        if (Id == Guid.Empty)
        {
            AddError("", "Product Id can't be empty", "");
        }
        if (!Title.IsValid())
        {
            AddErrors(Title.Errors);
        }
        if (String.IsNullOrEmpty(Sku))
        {
            AddError("", "Product Sku can't be empty", "");
        }
        if (String.IsNullOrEmpty(Description))
        {
            AddError("", "Product Description can't be empty", "");
        }
        if (Price < 0)
        {
            AddError("", "Price can't be negative", "");
        }
    }
}