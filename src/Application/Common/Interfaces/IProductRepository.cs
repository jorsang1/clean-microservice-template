using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product> Create(Product product);
    Task Delete(Product product);
    Task<List<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task Update(Product product);
    void Clear();
    Task<int> CountAsync();
}