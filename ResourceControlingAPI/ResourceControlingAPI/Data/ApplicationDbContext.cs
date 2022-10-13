using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Renter> Renters { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Meter> Meters { get; set; }

        public DbSet<MeterReading> MeterReadings { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
