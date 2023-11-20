using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Infrastructure.DataBase;
using DeliveryAPI.Infrastructure.UnitOfWork.Repositories;

namespace DeliveryAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;

        public IDeliveryRepository Deliveries { get; private set; }
        public ICourierRepository Couriers { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Deliveries = new DeliveryRepository(context);
            Couriers = new CourierRepository(context);  
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
