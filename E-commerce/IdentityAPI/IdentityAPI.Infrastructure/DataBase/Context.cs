using Microsoft.EntityFrameworkCore;
using IdentityAPI.Domain.Entities;

namespace IdentityAPI.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
