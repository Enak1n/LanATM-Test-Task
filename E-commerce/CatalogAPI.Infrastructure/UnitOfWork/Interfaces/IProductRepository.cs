using CatalogAPI.Domain.Entities;

namespace CatalogAPI.Infrastructure.UnitOfWork.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
