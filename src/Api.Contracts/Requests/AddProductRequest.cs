namespace CleanCompanyName.DDDMicroservice.Api.Contracts.Requests;

public class AddProductRequest
{
    public Guid Id { get; set; }
    public string Sku { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public AddProductRequest(Guid id, string sku, string title, string? description, decimal price)
    {
        Id = id;
        Sku = sku;
        Title = title;
        Description = description;
        Price = price;
    }
}