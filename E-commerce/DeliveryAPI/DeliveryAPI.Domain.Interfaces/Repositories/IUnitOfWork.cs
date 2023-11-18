namespace DeliveryAPI.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDeliveryRepository Deliveries { get; }

        Task SaveChangesAsync();
    }
}
