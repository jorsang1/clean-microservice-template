using Domain.Entities.Product;

namespace Application.Common.Interfaces;
public interface IProductRepository
{
    Task<Product> Create(Product product);
    Task Delete(Product product);
    Task<List<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task Update(Product product);
}