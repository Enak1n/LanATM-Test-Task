namespace IdentityAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITokenRepository Tokens { get; }
        IAddressRepository Addresses { get; }

        Task SaveChangesAsync();
    }
}
