using CatalogAPI.Domain.Entities;
using CatalogAPI.Infrastructure.DataBase;
using СatalogAPI.Domain.Intefaces.Repositpries;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context) : base(context) { }
    }
}
