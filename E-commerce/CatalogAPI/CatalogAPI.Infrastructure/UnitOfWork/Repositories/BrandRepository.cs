using CatalogAPI.Domain.Entities;
using CatalogAPI.Infrastructure.DataBase;
using CatalogAPI.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(Context context) : base(context) { }
    }
}
