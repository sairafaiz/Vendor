using Microsoft.EntityFrameworkCore;
using VendorMVC.Entities;

namespace VendorMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {

        }

        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<VendorAddress> vendorAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
