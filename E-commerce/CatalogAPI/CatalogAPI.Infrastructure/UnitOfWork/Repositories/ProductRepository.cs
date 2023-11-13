using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Infrastructure.DataBase;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context) { }
    }
}
