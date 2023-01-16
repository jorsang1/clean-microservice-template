namespace CleanCompanyName.DDDMicroservice.Api.Contracts.Responses;

public class ProductListItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public ProductListItemResponse(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}