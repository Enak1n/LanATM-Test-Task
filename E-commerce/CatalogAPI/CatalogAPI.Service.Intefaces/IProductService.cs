using CatalogAPI.Domain.Entities;

namespace CatalogAPI.Service.Intefaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(Guid id);
        Task<Product> GetByName(string name);
        Task<Product> Create(Product product);
        Task Update(Product product);
    }
}
