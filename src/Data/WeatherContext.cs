using Microsoft.EntityFrameworkCore;
using WeatherDataService.Models;

namespace WeatherDataService.Data 
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This tells EF Core to map Current's properties into WeatherData's table
            modelBuilder.Entity<WeatherData>().OwnsOne(w => w.current);

            base.OnModelCreating(modelBuilder); // Important to call base method
        }
    }
}
