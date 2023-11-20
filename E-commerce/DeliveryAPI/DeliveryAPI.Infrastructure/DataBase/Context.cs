using DeliveryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Courier> Couriers { get; set; }
    }
}
