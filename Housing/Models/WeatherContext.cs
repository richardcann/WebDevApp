using Microsoft.EntityFrameworkCore;

namespace Housing.Models
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherForecast> Weather { get; set; }
    }
}
