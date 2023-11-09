using CatalogAPI.Domain.Entities;
using CatalogAPI.Infrastructure.DataBase;
using CatalogAPI.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context) { }
    }
}
