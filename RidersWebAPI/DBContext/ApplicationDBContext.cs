using Microsoft.EntityFrameworkCore;
using RidersWebAPI.Models;

namespace RidersWebAPI.DBContext
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ride>()
                .HasKey(r => r.Id);
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DriverLocation> DriverLocations { get; set; }
        public DbSet<RideRequest> RideRequests { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<RideStatusHistory> RideStatusHistories { get; set; }
    }
}
