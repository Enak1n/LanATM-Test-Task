namespace OrderAPI.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IAddressRepository Addresses { get; }

        Task SaveChangesAsync();
    }
}
