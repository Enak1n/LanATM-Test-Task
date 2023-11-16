using Microsoft.EntityFrameworkCore;
using OrderAPI.Domain.Entities;

namespace OrderAPI.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {       
        }

        public DbSet<Order> Orders { get; set; }
    }
}
