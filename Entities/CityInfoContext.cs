using Microsoft.EntityFrameworkCore;

namespace CityInfoApi.Entities
{
    public class CityInfoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Startup.Configuration["connectionStrings:cityDBConnectionString"]);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}