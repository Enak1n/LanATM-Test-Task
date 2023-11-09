using CatalogAPI.Infrastructure.DataBase;
using CatalogAPI.Infrastructure.UnitOfWork.Interfaces;
using CatalogAPI.Infrastructure.UnitOfWork.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public IProductRepository Products { get; private set; }

        public IBrandRepository Brands { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Products = new ProductRepository(context);
            Brands = new BrandRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
