using IdentityAPI.DataBase.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAPI.DataBase
{
    public class CustomUserStore : UserStore<User>, ICustomUserStore, IDisposable
    {
        private readonly Context _context;

        public override Context Context { get => _context; }

        public CustomUserStore(Context context) : base(context)
        {
            AutoSaveChanges = true;
            _context = context;
        }

        public async Task AddRangeTokenAsync(IEnumerable<Token> tokens)
        {
            await _context.Tokens.AddRangeAsync(tokens);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Token>> BlockTokens(Guid userId)
        {

            var accessToken = _context.Tokens.FirstOrDefault(x => x.UserId == userId && x.TokenType == TokenType.Access
                                                             && x.IsActive);

            var refreshToken = _context.Tokens.FirstOrDefault(x => x.UserId == userId && x.TokenType == TokenType.Refresh
                                                              && x.IsActive);
            if (accessToken != null)
            {
                accessToken.IsActive = false;
            }

            if (refreshToken != null)
            {
                refreshToken.IsActive = false;
            }

            await _context.SaveChangesAsync();

            return new List<Token> { accessToken, refreshToken };
        }

        public async Task<List<Token>> GetTokensByUserId(Guid userId)
        {
            return await _context.Tokens.Where(x => x.UserId == userId && x.IsActive).ToListAsync();
        }

        public async Task<Token> GetToken(string value)
        {
            return _context.Tokens.FirstOrDefault(token => token.Value == value);
        }
    }
}
