using BIKERENTALAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BIKERENTALAPI.Database
{
    public class BIKEDbcontext : DbContext
    {
        public BIKEDbcontext(DbContextOptions<BIKEDbcontext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

    }
}
