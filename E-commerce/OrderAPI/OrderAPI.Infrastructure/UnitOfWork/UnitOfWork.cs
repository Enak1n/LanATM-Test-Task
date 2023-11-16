using OrderAPI.Domain.Interfaces;
using OrderAPI.Infrastructure.DataBase;
using OrderAPI.Infrastructure.UnitOfWork.Repositories;

namespace OrderAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Orders = new OrderRepository(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
