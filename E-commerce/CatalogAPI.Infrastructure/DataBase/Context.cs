using CatalogAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}
