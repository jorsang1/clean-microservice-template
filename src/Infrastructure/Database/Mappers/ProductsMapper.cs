namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Mappers;

internal static class ProductsMapper
{
    public static Domain.Entities.Product.Product? MapToEntity(this Database.Models.Product dto)
    {
        if (dto is null)
            return null;

        return new Domain.Entities.Product.Product
        {
            Id = dto.Id,
            Sku = dto.Sku,
            Title = new Domain.Entities.Product.ValueObjects.ProjectTitle(dto.Title),
            Description = dto.Description,
            Price = dto.Price,
            CreatedOn = dto.CreationDate,
            CreatedBy = dto.CreationBy,
            LastModifiedOn = dto.LastUpdate,
            LastModifiedBy = dto.LastUpdateBy,
        };
    }
}