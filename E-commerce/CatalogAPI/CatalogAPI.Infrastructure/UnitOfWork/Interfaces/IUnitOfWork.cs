using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPI.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IBrandRepository Brands { get; }

        Task SaveChangesAsync();
    }
}
