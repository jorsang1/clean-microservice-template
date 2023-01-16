using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class UpdateProductCommandBuilder
{
    public static UpdateProductCommand GetUpdateProductCommandEmpty()
    {
        return new UpdateProductCommand
        (
            Id: default,
            Sku: default!,
            Title: default!,
            Description: default!,
            Price: default,
            UserId: default
        );
    }

    public static UpdateProductCommand GetUpdateProductCommandWithTitle(string title = "UpdateProductCommand title")
    {
        return GetUpdateProductCommandEmpty() with
        {
            Title = title
        };
    }
}
