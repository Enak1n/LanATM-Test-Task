using IdentityAPI.DataBase.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.DataBase
{
    public interface ICustomUserStore : IUserStore<User>
    {
        public Task AddRangeTokenAsync(IEnumerable<Token> tokens);
        public Task<List<Token>> BlockTokens(Guid userId);
        public Task<List<Token>> GetTokensByUserId(Guid userId);
        Task<Token> GetToken(string value);
    }
}
