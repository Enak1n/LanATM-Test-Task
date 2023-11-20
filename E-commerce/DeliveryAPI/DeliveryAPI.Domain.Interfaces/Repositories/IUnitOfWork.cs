namespace DeliveryAPI.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDeliveryRepository Deliveries { get; }
        ICourierRepository Couriers { get; }

        Task SaveChangesAsync();
    }
}
