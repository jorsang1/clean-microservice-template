using CleanCompanyName.DDDMicroservice.Domain.Common;

namespace CleanCompanyName.DDDMicroservice.Domain.Entities.Product.ValueObjects;

public record struct ProjectTitle(string Title);
/*public class ProjectTitle : ValueObject
{
    public string Value { get; }

    public ProjectTitle(string title)
    {
        Value = title;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}*/