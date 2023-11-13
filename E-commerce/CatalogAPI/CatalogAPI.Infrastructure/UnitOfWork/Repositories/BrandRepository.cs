using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Infrastructure.DataBase;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(Context context) : base(context) { }
    }
}
